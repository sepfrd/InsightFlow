using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence.Repositories;

public class BlogPostRepository : RepositoryBase<BlogPost, long>, IBlogPostRepository
{
    public BlogPostRepository(DbSet<BlogPost> dbSet) : base(dbSet)
    {
    }
}