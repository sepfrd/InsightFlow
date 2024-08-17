using InsightFlow.DataAccess.Base;
using InsightFlow.DataAccess.Context;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Sieve.Services;

namespace InsightFlow.DataAccess.Repositories;

public class UserRepository : BaseRepository<User>
{
    private readonly DbSet<UserRole> _userRoles;
    private readonly DbSet<Bookmark> _bookmarks;

    public UserRepository(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor) :
        base(dbContext, sieveProcessor)
    {
        _userRoles = dbContext.Set<UserRole>();
        _bookmarks = dbContext.Set<Bookmark>();
    }

    public async Task<bool> CreateUserRoleAsync(UserRole userRole, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _userRoles.AddAsync(userRole, cancellationToken);

        return entityEntry.State == EntityState.Added;
    }

    public bool DeleteUserRole(UserRole userRole)
    {
        var entityEntry = _userRoles.Remove(userRole);

        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> CreateBookmarkAsync(Bookmark bookmark, CancellationToken cancellationToken = default)
    {
        var entityEntry = await _bookmarks.AddAsync(bookmark, cancellationToken);

        return entityEntry.State == EntityState.Added;
    }

    public bool DeleteBookmark(Bookmark bookmark)
    {
        var entityEntry = _bookmarks.Remove(bookmark);

        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<List<Role?>> GetRolesAsync(int userId, CancellationToken cancellationToken = default) =>
        await _userRoles
            .Where(x => x.UserId == userId)
            .Include(x => x.Role)
            .Select(x => x.Role)
            .ToListAsync(cancellationToken);
}