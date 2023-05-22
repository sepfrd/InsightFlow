using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.EntityFrameworkCore;
using NLog;
using NLog.Web;
using RedditMockup.DataAccess.Context;
using RedditMockup.Service.Grpc;
using RedditMockup.Web;
// TODO: Use logging across the app
// TODO: Use redis
var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Logging.ClearProviders();

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
        .InjectGrpc()
        .AddHealthChecks();

    //.InjectRedis(builder.Configuration)

    builder
        .WebHost
        .ConfigureKestrel(options =>
        {
            options.ListenLocalhost(6000, o => o.Protocols = HttpProtocols.Http2);
            options.ListenLocalhost(6001, o => o.Protocols = HttpProtocols.Http1AndHttp2);
        });
        


    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    using var context = scope.ServiceProvider.GetRequiredService<RedditMockupContext>();

    app.UseSwagger()
        .UseSwaggerUI();

    if (app.Environment.IsEnvironment("Testing"))
    {
        await context.Database.EnsureDeletedAsync();
    }

    if (!app.Environment.IsProduction())
    {
        await context.Database.EnsureCreatedAsync();
    }

    else
    {
        await context.Database.MigrateAsync();
        //app.UseHsts();
    }

    app
        //.UseHttpsRedirection()
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapHealthChecks("/healthcheck");
            endpoints.MapGrpcService<GrpcService>();
            endpoints.MapGet("/protos/redditmockup.proto", async context =>
            {
                await context.Response.WriteAsync(File.ReadAllText("../RedditMockup.Model/Protos/redditmockup.proto"));
            });
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