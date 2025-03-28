using System.Net;
using InsightFlow.Api.IntegrationTests.TestUtilities;
using Shouldly;

namespace InsightFlow.Api.IntegrationTests;

[Collection(Constants.DefaultTestCollectionName)]
public class HealthCheckTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    private readonly CustomWebApplicationFactory<Program> _customWebApplicationFactory;

    public HealthCheckTests(CustomWebApplicationFactory<Program> customWebApplicationFactory)
    {
        _customWebApplicationFactory = customWebApplicationFactory;
    }

    [Fact]
    public async Task GetHealthChecks_WhenApplicationIsHealthy_ShouldReturnHealthy()
    {
        // Arrange
        var httpClient = _customWebApplicationFactory.CreateClient();
        var healthCheckRequest = new HttpRequestMessage(HttpMethod.Get, Constants.HealthCheckAddress);

        // Act
        var healthCheckResponse = await httpClient.SendAsync(
            healthCheckRequest,
            TestContext.Current.CancellationToken);

        var healthCheckResponseString = await healthCheckResponse.Content.ReadAsStringAsync(TestContext.Current.CancellationToken);

        // Assert
        healthCheckResponse.StatusCode.ShouldBe(HttpStatusCode.OK);
        healthCheckResponseString.ShouldBe("Healthy");
    }
}