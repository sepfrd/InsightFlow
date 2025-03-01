using InsightFlow.Application.Interfaces.Repositories;

namespace InsightFlow.Application.Interfaces;

public interface IUnitOfWork
{
    IBlogPostRepository BlogPostRepository { get; }

    IUserRepository UserRepository { get; }

    IUserRoleRepository UserRoleRepository { get; }

    int CommitChanges();

    Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}