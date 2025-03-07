using Common.Resources;
using InsightFlow.Api.Extensions;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Json;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
    .MinimumLevel.Override("System", LogEventLevel.Information)
    .WriteTo.File(
        path: "./logs/internal/log-.json",
        rollingInterval: RollingInterval.Day,
        rollOnFileSizeLimit: true,
        formatter: new JsonFormatter())
    .Enrich.FromLogContext()
    .Enrich.WithMachineName()
    .Enrich.WithThreadId()
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();

    builder.Services.AddDependencies(builder.Configuration, builder.Environment);

    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    await using var dbContext = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

    if (app.Environment.IsEnvironment(ApplicationConstants.TestingEnvironmentName))
    {
        await dbContext.Database.EnsureDeletedAsync();
        await dbContext.Database.EnsureCreatedAsync();
    }
    else
    {
        await dbContext.Database.MigrateAsync();
    }

    app.UseExceptionHandler();

    app.MapOpenApi();

    app.MapScalarApiReference(options =>
    {
        options.DarkMode = true;
        options.Theme = ScalarTheme.BluePlanet;
        options.Title = ApplicationConstants.ApplicationName;
    });

    app
        .UseHsts()
        .UseHttpsRedirection()
        .UseRouting()
        .UseRateLimiter()
        .UseCors(!app.Environment.IsProduction() ? ApplicationConstants.AllowAnyOriginCorsPolicy : ApplicationConstants.RestrictedCorsPolicy)
        .UseAuthentication()
        .UseAuthorization();

    app.MapControllers();

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Fatal(exception, ApplicationMessages.ApplicationFatalException, exception.GetType());
}
finally
{
    await Log.CloseAndFlushAsync();
}