using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using RedditMockup.DataAccess.Context;
using RedditMockup.Web;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Host.UseNLog();

var logger = NLogBuilder
        .ConfigureNLog("nlog.config")
        .GetLogger("Default");

try
{
    builder.Services
        .AddEndpointsApiExplorer()
        .InjectApi()
        .InjectSwagger()
        .InjectUnitOfWork()
        .InjectSieve()
        .InjectAuthentication()
        .InjectNLog(builder.Environment)
        .InjectContext(builder.Configuration, builder.Environment)
        .InjectBusinesses()
        .InjectFluentValidation()
        .InjectRabbitMq()
        .InjectAutoMapper()
        .AddHealthChecks();
    //.InjectRedis(builder.Configuration)

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
        //app.UseExceptionHandler();
        app.UseHsts();
    }

    app
        .UseHttpsRedirection()
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
    logger.Error(exception, $"Program stopped due to a {exception.GetType()} exception.");
    throw;
}
finally
{
    LogManager.Shutdown();
}

public partial class Program
{
}