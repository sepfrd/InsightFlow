using InsightFlow.Api.Extensions;
using InsightFlow.Api.Middlewares;
using InsightFlow.Common.Constants;
using InsightFlow.Infrastructure.Common.Configurations;
using InsightFlow.Infrastructure.Common.Constants;
using Microsoft.Extensions.Options;
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
    .CreateBootstrapLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    builder.Logging.ClearProviders();

    builder.Services.AddDependencies(builder.Configuration, builder.Environment);

    var app = builder.Build();

    await app.InitializeDatabaseAsync();

    var roleServiceInitializationResult = await app.InitializeRoleServiceAsync(10);

    if (!roleServiceInitializationResult)
    {
        var message = string.Format(StringConstants.RoleServiceInitializationFailureLog, 10);

        Log.Fatal(StringConstants.ApplicationInternalServerErrorTemplate, message);

        return;
    }

    var appOptions = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;

    var applicationVersion = appOptions.ApplicationInformation!.Version!.Split('.').First();

    applicationVersion = 'v' + applicationVersion;

    app.UseExceptionHandler();

    app.MapOpenApi(applicationVersion);

    app.MapScalarApiReference(options =>
    {
        options.DarkMode = true;
        options.Theme = ScalarTheme.BluePlanet;
        options.Title = appOptions.ApplicationInformation.Name!;
        options.OpenApiRoutePattern = applicationVersion;
    });

    if (app.Environment.IsProduction())
    {
        app
            .UseHsts()
            .UseHttpsRedirection();
    }

    app
        .UseRouting()
        .UseMiddleware<RequestSizeLimitMiddleware>()
        .UseRateLimiter()
        .UseCors(app.Environment.IsProduction()
            ? InfrastructureConstants.RestrictedCorsPolicy
            : InfrastructureConstants.AllowAnyOriginCorsPolicy)
        .UseAuthentication()
        .UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/health");

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Fatal(exception, StringConstants.ApplicationFatalExceptionTemplate, exception.GetType());
}
finally
{
    await Log.CloseAndFlushAsync();
}

public partial class Program;