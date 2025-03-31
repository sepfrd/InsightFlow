using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class BlogPostRepository : RepositoryBase<BlogPost, long>, IBlogPostRepository
{
    public BlogPostRepository(DbSet<BlogPost> dbSet, IDbConnectionPool dbConnectionPool) : base(dbSet, dbConnectionPool)
    {
    }
}