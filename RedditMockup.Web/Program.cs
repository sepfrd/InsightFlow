using Microsoft.EntityFrameworkCore;
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
    .CreateBootstrapLogger();

Log.Information("Application setup started.");

try
{
    builder.Services
        .AddEndpointsApiExplorer()
        .InjectApi()
        .InjectCors()
        .InjectSwagger()
        .InjectUnitOfWork()
        .InjectSieve()
        .InjectSerilog(builder.Configuration)
        .InjectAuthentication()
        .InjectContext(builder.Configuration, builder.Environment)
        .InjectBusinesses()
        .InjectFluentValidation()
        .InjectAutoMapper()
        .AddHealthChecks();

    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    await using var context = scope.ServiceProvider.GetRequiredService<RedditMockupContext>();

    app.UseSwagger()
        .UseSwaggerUI();

    if (app.Environment.IsEnvironment("Testing"))
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
        .UseCors("AllowAnyOrigin")
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthcheck");
        });
    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Error(exception, "Program stopped due to a {ExceptionType} exception", exception.GetType());
    throw;
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program
{
}