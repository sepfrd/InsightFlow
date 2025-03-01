using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class RoleRepository : RepositoryBase<Role, long>, IRoleRepository
{
    public RoleRepository(DbSet<Role> dbSet) : base(dbSet)
    {
    }
}