using Microsoft.EntityFrameworkCore;
using RedditMockup.DataAccess.Context;
using RedditMockup.Service.Grpc;
using RedditMockup.Web;
using Serilog;
using Serilog.Settings.Configuration;

// TODO: Use logging across the app

// TODO: Setup the JSON format

// TODO: Use redis

var builder = WebApplication.CreateBuilder(args);

#region [macOS Configuration for gRPC over HTTP 2.0 Without TLS]

/*

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.

    options.ListenLocalhost(6000, o => o.Protocols =
    HttpProtocols.Http2);
});

*/

#endregion

builder.Configuration.AddEnvironmentVariables();

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(builder.Configuration, new ConfigurationReaderOptions
    {
        SectionName = "InternalSerilog"
    })
    .CreateBootstrapLogger();

Log.Information("Hello world!!!");

try
{
    if (!builder.Environment.IsEnvironment("NoK8S"))
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
            .InjectMongoDbSettings(builder.Configuration)
            .InjectContext(builder.Configuration, builder.Environment)
            .InjectBusinesses()
            .InjectFluentValidation()
            .InjectRabbitMq()
            .InjectAutoMapper()
            .InjectGrpc()
            .AddHealthChecks();

        //.InjectRedis(builder.Configuration)
    }
    else
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
    }


    var app = builder.Build();

    await using var scope = app.Services.CreateAsyncScope();

    await using var context = scope.ServiceProvider.GetRequiredService<RedditMockupContext>();

    app.UseSwagger()
        .UseSwaggerUI();

    if (app.Environment.IsEnvironment("Testing") || app.Environment.IsEnvironment("NoK8S"))
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

            if (!app.Environment.IsEnvironment("NoK8S"))
            {
                endpoints.MapGrpcService<GrpcService>();
                endpoints.MapGet("/protos/redditmockup.proto", async httpContext =>
                {
                    await httpContext.Response.WriteAsync(File.ReadAllText("../RedditMockup.Model/Protos/redditmockup.proto"));
                });
            }
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