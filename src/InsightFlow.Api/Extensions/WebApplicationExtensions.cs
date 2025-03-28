using InsightFlow.Application.Interfaces;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Api.Extensions;

public static class WebApplicationExtensions
{
    public async static Task<bool> InitializeRoleServiceAsync(this WebApplication webApplication, int numberOfRetries)
    {
        var roleService = webApplication.Services.GetRequiredService<IRoleService>();

        var initializationResult = false;

        for (var i = 0; !initializationResult && i < numberOfRetries; i++)
        {
            initializationResult = await roleService.InitializeAsync();
        }

        return initializationResult;
    }

    public async static Task InitializeDatabaseAsync(this WebApplication webApplication)
    {
        await using var scope = webApplication.Services.CreateAsyncScope();

        await using var dbContext = scope.ServiceProvider.GetRequiredService<UnitOfWork>();

        if (webApplication.Environment.IsEnvironment(InfrastructureConstants.TestingEnvironmentName))
        {
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();
        }
        else
        {
            await dbContext.Database.MigrateAsync();
        }
    }
}