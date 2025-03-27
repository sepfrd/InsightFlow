using InsightFlow.Infrastructure.Persistence;

namespace InsightFlow.Api.IntegrationTests.TestUtilities;

public class DatabaseUtilities
{
    public static void ResetDatabase(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;

        var dbContext = scopedServices.GetRequiredService<UnitOfWork>();

        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
}