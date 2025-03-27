using InsightFlow.Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Mvc.Testing;

namespace InsightFlow.Api.IntegrationTests.TestUtilities;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    private const string ConfigurationFilePath = "appsettings.Test.json";

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(ApplicationConstants.TestingEnvironmentName);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile(ConfigurationFilePath)
            .AddEnvironmentVariables()
            .Build();

        builder.UseConfiguration(configuration);
    }
}