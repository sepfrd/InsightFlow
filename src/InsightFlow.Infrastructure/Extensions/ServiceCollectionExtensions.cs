using FluentValidation;
using InsightFlow.Application.Interfaces;
using InsightFlow.Infrastructure.Common.Dtos;
using InsightFlow.Infrastructure.Interfaces;
using InsightFlow.Infrastructure.Persistence;
using InsightFlow.Infrastructure.Services;
using InsightFlow.Infrastructure.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace InsightFlow.Infrastructure.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services) =>
        services
            .AddSingleton<IMappingService, MappingService>()
            .AddSingleton<IDataValidator<CreateUserRequestDto>, DataValidator<CreateUserRequestDto>>()
            .AddScoped<IAuthService, AuthService>()
            .AddValidatorsFromAssemblyContaining<CreateUserRequestDtoValidator>()
            .AddDbContext<IUnitOfWork, UnitOfWork>(options => options.UseInMemoryDatabase(nameof(InsightFlow)));
}