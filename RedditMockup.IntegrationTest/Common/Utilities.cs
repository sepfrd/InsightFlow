using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using RedditMockup.DataAccess.Context;

namespace RedditMockup.IntegrationTest.Common;

public class Utilities
{
    public static void ResetDatabase(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        
        var dbContext = scopedServices.GetRequiredService<RedditMockupDbContext>();
        
        dbContext.Database.EnsureDeleted();
        dbContext.Database.EnsureCreated();
    }
    
    public static Guid GetValidAnswerGuid(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;
        
        var dbContext = scopedServices.GetRequiredService<RedditMockupDbContext>();

        var validGuid = dbContext.Answers!.First().Guid;

        return validGuid;
    }

}