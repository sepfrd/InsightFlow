using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class UserRepository : RepositoryBase<User, long>, IUserRepository
{
    public UserRepository(DbSet<User> dbSet) : base(dbSet)
    {
    }

    public async Task<bool> IsEmailUniqueAsync(string email, CancellationToken cancellationToken = default)
    {
        var user = await _dbSet.FirstOrDefaultAsync(
            user => user.Email == email,
            cancellationToken);

        return user is null;
    }

    public async Task<bool> IsUsernameUniqueAsync(string username, CancellationToken cancellationToken = default)
    {
        var user = await _dbSet.FirstOrDefaultAsync(
            user => user.Username == username,
            cancellationToken);

        return user is null;
    }
}