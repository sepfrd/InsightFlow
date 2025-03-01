using InsightFlow.Application.Features.BlogPosts.Commands.Handlers;
using InsightFlow.Infrastructure.Extensions;

namespace InsightFlow.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services)
    {
        services
            .AddOpenApi()
            .AddHttpContextAccessor()
            .AddInfrastructure()
            .AddMediatR(config => config.RegisterServicesFromAssemblyContaining<CreateBlogPostCommandHandler>())
            .AddControllers();

        return services;
    }
}