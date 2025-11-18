using System.Reflection;
using InsightFlow.Common.Cqrs;
using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Common.Cqrs.Queries;
using Microsoft.Extensions.DependencyInjection;

namespace InsightFlow.Common;

public static class ServiceCollectionExtensions
{
    extension(IServiceCollection services)
    {
        public IServiceCollection AddCqrs(params Assembly[] assemblies) =>
            services
                .Scan(scan => scan
                    .FromAssemblies(assemblies.Append(Assembly.GetExecutingAssembly()))
                    .AddClasses(
                        classes => classes.AssignableTo(typeof(IQueryHandler<,>)),
                        publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(
                        classes => classes.AssignableTo(typeof(ICommandHandler<,>)),
                        publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime()
                    .AddClasses(
                        classes => classes.AssignableTo<IMediator>(),
                        publicOnly: false)
                    .AsImplementedInterfaces()
                    .WithScopedLifetime());
    }
}