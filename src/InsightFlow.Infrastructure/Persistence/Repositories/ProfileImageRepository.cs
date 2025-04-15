using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class ProfileImageRepository : RepositoryBase<ProfileImage, long>, IProfileImageRepository
{
    public ProfileImageRepository(DbSet<ProfileImage> dbSet, IDbConnectionPool dbConnectionPool)
        : base(dbSet, dbConnectionPool)
    {
    }
}