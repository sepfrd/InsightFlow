using InsightFlow.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace InsightFlow.Infrastructure.Services;

public class RoleService : IRoleService
{
    private readonly Dictionary<string, long> _roleCache = new();
    private readonly IServiceProvider _serviceProvider;

    public RoleService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task<bool> InitializeAsync(CancellationToken cancellationToken = default)
    {
        await using var scope = _serviceProvider.CreateAsyncScope();

        var unitOfWork = scope.ServiceProvider.GetRequiredService<IUnitOfWork>();

        _roleCache.Clear();

        var rolesResponse = await unitOfWork
            .RoleRepository
            .GetAllAsync(
                page: 1,
                limit: int.MaxValue,
                cancellationToken: cancellationToken);

        if (!rolesResponse.IsSuccess ||
            rolesResponse.Data is null ||
            !rolesResponse.Data.Any())
        {
            return false;
        }

        foreach (var role in rolesResponse.Data)
        {
            _roleCache[role.Title] = role.Id;
        }

        return true;
    }

    public long? GetRoleIdByRoleTitle(string roleTitle)
    {
        if (_roleCache.TryGetValue(roleTitle, out var roleId))
        {
            return roleId;
        }

        return null;
    }
}