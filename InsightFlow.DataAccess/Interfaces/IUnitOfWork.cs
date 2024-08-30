using InsightFlow.Model.Entities;

namespace InsightFlow.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Answer> AnswerRepository { get; }

    IBaseRepository<Question> QuestionRepository { get; }

    IBaseRepository<User> UserRepository { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}