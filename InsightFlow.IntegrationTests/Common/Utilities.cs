using InsightFlow.DataAccess.Context;
using Microsoft.Extensions.DependencyInjection;

namespace InsightFlow.IntegrationTests.Common;

public class Utilities
{
    public static void ResetDatabase(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;

        var dbContext = scopedServices.GetRequiredService<InsightFlowDbContext>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}