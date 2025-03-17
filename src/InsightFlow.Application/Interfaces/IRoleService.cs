namespace InsightFlow.Application.Interfaces;

public interface IRoleService
{
    Task<bool> InitializeAsync(CancellationToken cancellationToken = default);

    long? GetRoleIdByRoleTitle(string roleTitle);
}