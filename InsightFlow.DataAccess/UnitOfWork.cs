using InsightFlow.DataAccess.Base;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private IBaseRepository<Answer>? _answerRepository;
    private IBaseRepository<Question>? _questionRepository;
    private IBaseRepository<User>? _userRepository;

    private readonly InsightFlowDbContext _dbContext;
    private readonly ISieveProcessor _sieveProcessor;

    public UnitOfWork(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor)
    {
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }

    public IBaseRepository<Answer> AnswerRepository =>
        _answerRepository ??= new BaseRepository<Answer>(_dbContext, _sieveProcessor);

    public IBaseRepository<Question> QuestionRepository =>
        _questionRepository ??= new BaseRepository<Question>(_dbContext, _sieveProcessor);

    public IBaseRepository<User> UserRepository =>
        _userRepository ??= new BaseRepository<User>(_dbContext, _sieveProcessor);

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}