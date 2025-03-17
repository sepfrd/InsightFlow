using InsightFlow.Domain.Entities;

namespace InsightFlow.Application.Interfaces.Repositories;

public interface IUserRepository : IRepositoryBase<User, long>
{
    Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default);

    Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default);
}