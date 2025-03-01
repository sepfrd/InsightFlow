using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class UserRepository : RepositoryBase<User, long>, IUserRepository
{
    public UserRepository(DbSet<User> dbSet) : base(dbSet)
    {
    }
}