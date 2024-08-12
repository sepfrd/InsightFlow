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

    public static Guid GetValidQuestionGuid(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;

        var dbContext = scopedServices.GetRequiredService<RedditMockupDbContext>();

        var validGuid = dbContext.Questions!.First().Guid;

        return validGuid;
    }

    public static Guid GetAdminGuid(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;

        var dbContext = scopedServices.GetRequiredService<RedditMockupDbContext>();

        var adminGuid = dbContext.Users!.First(user => user.Username == Constants.AdminUsername).Guid;

        return adminGuid;
    }

    public static Guid GetUserGuid(CustomWebApplicationFactory<Program> factory)
    {
        using var scope = factory.Services.CreateScope();

        var scopedServices = scope.ServiceProvider;

        var dbContext = scopedServices.GetRequiredService<RedditMockupDbContext>();

        var userGuid = dbContext.Users!.First(user => user.Username == Constants.UserUsername).Guid;

        return userGuid;
    }
}