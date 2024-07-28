using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;

namespace RedditMockup.DataAccess.Contracts;

public interface IUnitOfWork
{
    IBaseRepository<Answer>? AnswerRepository { get; }

    IBaseRepository<Person>? PersonRepository { get; }

    IBaseRepository<Question>? QuestionRepository { get; }

    IBaseRepository<Role>? RoleRepository { get; }

    IBaseRepository<User>? UserRepository { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken);
}