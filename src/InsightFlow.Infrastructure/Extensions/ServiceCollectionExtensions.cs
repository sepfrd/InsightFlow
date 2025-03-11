using FluentValidation;
using InsightFlow.Application.Features.BlogPosts.Dtos;
using InsightFlow.Application.Features.Users.Dtos;
using InsightFlow.Application.Interfaces;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Common.Constants;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Common.Helpers;
using InsightFlow.Infrastructure.Interfaces;
using InsightFlow.Infrastructure.Persistence;
using InsightFlow.Infrastructure.Services;
using InsightFlow.Infrastructure.Validators;
using Mapster;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace InsightFlow.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        ConfigureMapster();

        return services
            .AddSingleton<IMappingService, MappingService>()
            .AddSingleton<IDataValidator<CreateUserRequestDto>, DataValidator<CreateUserRequestDto>>()
            .AddValidatorsFromAssemblyContaining<CreateUserRequestDtoValidator>(ServiceLifetime.Singleton)
            .AddScoped<IAuthService, AuthService>()
            .AddDatabase(configuration, environment);
    }

    private static void ConfigureMapster()
    {
        TypeAdapterConfig<BlogPost, BlogPostResponseDto>
            .ForType()
            .Map(
                dto => dto.AuthorUuid,
                blogPost => blogPost.Author!.Uuid);

        TypeAdapterConfig<User, UserResponseDto>
            .ForType()
            .Map(
                dto => dto.FullName,
                user => user.FirstName + ' ' + user.LastName);
    }

    private static IServiceCollection AddDatabase(
        this IServiceCollection services,
        IConfiguration configuration,
        IWebHostEnvironment environment)
    {
        if (environment.IsEnvironment(ApplicationConstants.TestingEnvironmentName))
        {
            return services.AddDbContext<IUnitOfWork, UnitOfWork>(options =>
                options.UseInMemoryDatabase(ApplicationConstants.ApplicationName));
        }

        return services.AddDbContext<IUnitOfWork, UnitOfWork>(options =>
        {
            options
                .UseSqlServer(configuration.GetConnectionString(ApplicationConstants.SqlServerConfigurationKey))
                .UseSeeding((dbContext, _) => dbContext.SeedDatabase())
                .UseAsyncSeeding((dbContext, _, _) => Task.FromResult(dbContext.SeedDatabase()));

            if (!environment.IsProduction())
            {
                options.EnableSensitiveDataLogging();
            }
        });
    }

    private static DbContext SeedDatabase(this DbContext dbContext)
    {
        var rolesDbSet = dbContext.Set<Role>();

        if (!rolesDbSet.Any())
        {
            rolesDbSet.AddRange(
                new Role
                {
                    Title = ApplicationConstants.AdminRoleName,
                    Description = "Administrator of the Application"
                },
                new Role
                {
                    Title = ApplicationConstants.UserRoleName,
                    Description = "Basic User of the Application"
                });

            dbContext.SaveChanges();
        }

        if (dbContext.Set<User>().Any())
        {
            return dbContext;
        }

        const int fakeUsersCount = 1_000;
        const int fakeBlogPostsCount = 1_000;

        var fakeUsers = FakeDataHelper.GetFakeUsers(fakeUsersCount);

        dbContext.Set<User>().AddRange(fakeUsers);

        dbContext.SaveChanges();

        var fakeBlogPosts = FakeDataHelper.GetFakeBlogPosts(fakeUsers, fakeBlogPostsCount);

        dbContext.Set<BlogPost>().AddRange(fakeBlogPosts);

        dbContext.SaveChanges();

        var fakeUserRoles = FakeDataHelper.GetFakeUserRoles(fakeUsers, dbContext.Set<Role>().ToList());

        dbContext.Set<UserRole>().AddRange(fakeUserRoles);

        dbContext.SaveChanges();

        return dbContext;
    }
}