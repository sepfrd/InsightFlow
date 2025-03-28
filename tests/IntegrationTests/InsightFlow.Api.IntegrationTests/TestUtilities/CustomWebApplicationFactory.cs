using InsightFlow.Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;

namespace InsightFlow.Api.IntegrationTests.TestUtilities;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private const string ConfigurationFilePath = "/Users/sepehr/Projects/InsightFlow/tests/IntegrationTests/InsightFlow.Api.IntegrationTests/appsettings.Test.json";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(ApplicationConstants.TestingEnvironmentName);

        var configuration = new ConfigurationBuilder()
            .AddJsonFile(ConfigurationFilePath)
            .AddEnvironmentVariables()
            .Build();

        builder.UseConfiguration(configuration);
    }
}