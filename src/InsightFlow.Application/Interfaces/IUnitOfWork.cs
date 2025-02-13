using InsightFlow.Application.Interfaces.Repositories;

namespace InsightFlow.Application.Interfaces;

public interface IUnitOfWork
{
    IBlogPostRepository BlogPostRepository { get; }
    ICommentRepository CommentRepository { get; }
    IUserRepository UserRepository { get; }
    IUserRoleRepository UserRoleRepository { get; }

    int CommitChanges();

    Task<int> CommitChangesAsync(CancellationToken cancellationToken = default);
}
