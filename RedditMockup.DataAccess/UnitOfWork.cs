using Microsoft.Extensions.Options;
using RedditMockup.Common.Dtos;
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

    private readonly RedditMockupContext _context;

    private readonly ISieveProcessor _sieveProcessor;

    private readonly IOptions<MongoDbSettings> _mongoDbSettings;

    public UnitOfWork(RedditMockupContext context, ISieveProcessor sieveProcessor, IOptions<MongoDbSettings> mongoDbSettings)
    {
        _context = context;
        
        _sieveProcessor = sieveProcessor;

        _mongoDbSettings = mongoDbSettings;
    }

    public AnswerRepository AnswerRepository =>
        _answerRepository ??= new AnswerRepository(_context, _sieveProcessor, _mongoDbSettings);

    public PersonRepository PersonRepository =>
        _personRepository ??= new PersonRepository(_context, _sieveProcessor, _mongoDbSettings);


    public QuestionRepository QuestionRepository =>
        _questionRepository ??= new QuestionRepository(_context, _sieveProcessor, _mongoDbSettings);

    public RoleRepository RoleRepository =>
        _roleRepository ??= new RoleRepository(_context, _sieveProcessor, _mongoDbSettings);

    public UserRepository UserRepository =>
        _userRepository ??= new UserRepository(_context, _sieveProcessor, _mongoDbSettings);

    public async Task<int> CommitAsync(CancellationToken cancellationToken) =>
        await _context.SaveChangesAsync(cancellationToken);
}