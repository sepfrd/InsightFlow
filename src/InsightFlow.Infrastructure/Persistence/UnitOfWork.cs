using InsightFlow.Application.Interfaces;
using InsightFlow.Application.Interfaces.Repositories;
using InsightFlow.Domain.Entities;
using InsightFlow.Infrastructure.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.Infrastructure.Persistence;

public class UnitOfWork : DbContext, IUnitOfWork
{
    private BlogPostRepository? _blogPostRepository;
    private UserRepository? _userRepository;
    private UserRoleRepository? _userRoleRepository;

    public UnitOfWork(DbContextOptions dbContextOptions) : base(dbContextOptions)
    {
    }

    public DbSet<BlogPost> BlogPosts { get; set; }

    public DbSet<User> Users { get; set; }

    public DbSet<UserRole> UserRoles { get; set; }

    public IBlogPostRepository BlogPostRepository => _blogPostRepository ??= new BlogPostRepository(BlogPosts);

    public IUserRepository UserRepository => _userRepository ??= new UserRepository(Users);

    public IUserRoleRepository UserRoleRepository => _userRoleRepository ??= new UserRoleRepository(UserRoles);

    public int CommitChanges() => SaveChanges();

    public async Task<int> CommitChangesAsync(CancellationToken cancellationToken = default) =>
        await SaveChangesAsync(cancellationToken);
}