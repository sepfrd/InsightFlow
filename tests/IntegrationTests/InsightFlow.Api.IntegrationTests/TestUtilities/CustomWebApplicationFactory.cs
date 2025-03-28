using InsightFlow.Infrastructure.Common.Constants;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace InsightFlow.Api.IntegrationTests.TestUtilities;

public class CustomWebApplicationFactory<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment(InfrastructureConstants.TestingEnvironmentName);
    }
}