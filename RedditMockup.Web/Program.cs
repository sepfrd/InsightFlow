using Microsoft.EntityFrameworkCore;
using RedditMockup.Common.Constants;
using RedditMockup.DataAccess.Context;
using RedditMockup.Web;
using Serilog;
using Serilog.Settings.Configuration;

// TODO: Use logging across the app

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration, new ConfigurationReaderOptions
    {
        SectionName = "InternalSerilog"
    })
    .CreateLogger();

Log.Information("Application setup started.");

try
{
    builder.Services
        .AddEndpointsApiExplorer()
        .InjectApiControllers()
        .AddHttpContextAccessor()
        .InjectCors(builder.Configuration)
        .InjectSwagger(builder.Configuration)
        .InjectUnitOfWork()
        .InjectSieve()
        .InjectSerilog(builder.Configuration)
        .InjectAuth(builder.Configuration)
        .InjectDbContext(builder.Configuration, builder.Environment)
        .InjectBusinesses()
        .InjectFluentValidation()
        .InjectAutoMapper()
        .AddHealthChecks();

    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    await using var context = scope.ServiceProvider.GetRequiredService<RedditMockupDbContext>();

    app.UseSwagger()
        .UseSwaggerUI();

    if (app.Environment.IsEnvironment(ApplicationConstants.TestingEnvironmentName))
    {
        await context.Database.EnsureDeletedAsync();
        await context.Database.EnsureCreatedAsync();
    }

    else
    {
        await context.Database.MigrateAsync();
        //app.UseHsts();
    }

    app
        //.UseHttpsRedirection()
        .UseCors(!app.Environment.IsProduction() ? ApplicationConstants.AllowAnyOriginCorsPolicy : ApplicationConstants.RestrictedCorsPolicy)
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization();

    app.MapControllers();
    app.MapHealthChecks("/health-checks");

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Fatal(exception, MessageConstants.ApplicationFatalExceptionMessage, exception.GetType());
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program;