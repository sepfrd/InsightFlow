using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using Sieve.Services;

namespace RedditMockup.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private AnswerRepository? _answerRepository;

    private PersonRepository? _personRepository;

    private QuestionRepository? _questionRepository;

    private RoleRepository? _roleRepository;

    private UserRepository? _userRepository;

    private readonly RedditMockupDbContext _dbContext;

    private readonly ISieveProcessor _sieveProcessor;

    public UnitOfWork(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor)
    {
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }

    public AnswerRepository AnswerRepository =>
        _answerRepository ??= new AnswerRepository(_dbContext, _sieveProcessor);

    public PersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(_dbContext, _sieveProcessor);

    public QuestionRepository QuestionRepository =>
        _questionRepository ??= new QuestionRepository(_dbContext, _sieveProcessor);

    public RoleRepository RoleRepository =>
        _roleRepository ??= new RoleRepository(_dbContext, _sieveProcessor);

    public UserRepository UserRepository =>
        _userRepository ??= new UserRepository(_dbContext, _sieveProcessor);

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}