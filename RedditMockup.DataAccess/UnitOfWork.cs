using RedditMockup.DataAccess.Context;
using RedditMockup.DataAccess.Contracts;
using RedditMockup.DataAccess.Repositories;
using RedditMockup.Model.Entities;
using Sieve.Services;

namespace RedditMockup.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private IBaseRepository<Answer>? _answerRepository;

    private IBaseRepository<Person>? _personRepository;

    private IBaseRepository<Question>? _questionRepository;

    private IBaseRepository<Role>? _roleRepository;

    private IBaseRepository<User>? _userRepository;

    private readonly RedditMockupDbContext _dbContext;

    private readonly ISieveProcessor _sieveProcessor;

    public UnitOfWork(RedditMockupDbContext dbContext, ISieveProcessor sieveProcessor)
    {
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }

    public IBaseRepository<Answer> AnswerRepository =>
        _answerRepository ??= new AnswerRepository(_dbContext, _sieveProcessor);

    public IBaseRepository<Person> PersonRepository =>
        _personRepository ??= new PersonRepository(_dbContext, _sieveProcessor);

    public IBaseRepository<Question> QuestionRepository =>
        _questionRepository ??= new QuestionRepository(_dbContext, _sieveProcessor);

    public IBaseRepository<Role> RoleRepository =>
        _roleRepository ??= new RoleRepository(_dbContext, _sieveProcessor);

    public IBaseRepository<User> UserRepository =>
        _userRepository ??= new UserRepository(_dbContext, _sieveProcessor);

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}