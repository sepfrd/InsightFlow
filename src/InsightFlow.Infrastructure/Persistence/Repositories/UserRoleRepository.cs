using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class UserRoleRepository : RepositoryBase<UserRole, long>, IUserRoleRepository
{
    public UserRoleRepository(DbSet<UserRole> dbSet, IDbConnectionPool dbConnectionPool) : base(dbSet, dbConnectionPool)
    {
    }
}