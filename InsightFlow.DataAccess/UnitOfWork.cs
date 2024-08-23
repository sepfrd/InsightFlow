using InsightFlow.DataAccess.Base;
using InsightFlow.DataAccess.Interfaces;
using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess;

public class UnitOfWork : IUnitOfWork
{
    private IBaseRepository<Answer>? _answerRepository;
    private IBaseRepository<EntityStateInformation>? _entityStateInformationRepository;
    private IBaseRepository<Person>? _personRepository;
    private IBaseRepository<Profile>? _profileRepository;
    private IBaseRepository<ProfileImage>? _profileImageRepository;
    private IBaseRepository<Question>? _questionRepository;
    private IBaseRepository<Role>? _roleRepository;
    private IBaseRepository<User>? _userRepository;
    private IBaseRepository<UserRole>? _userRoleRepository;

    private readonly InsightFlowDbContext _dbContext;
    private readonly ISieveProcessor _sieveProcessor;

    public UnitOfWork(InsightFlowDbContext dbContext, ISieveProcessor sieveProcessor)
    {
        _dbContext = dbContext;
        _sieveProcessor = sieveProcessor;
    }

    public IBaseRepository<Answer> AnswerRepository =>
        _answerRepository ??= new BaseRepository<Answer>(_dbContext, _sieveProcessor);

    public IBaseRepository<EntityStateInformation> EntityStateInformationRepository =>
        _entityStateInformationRepository ??= new BaseRepository<EntityStateInformation>(_dbContext, _sieveProcessor);

    public IBaseRepository<Person> PersonRepository =>
        _personRepository ??= new BaseRepository<Person>(_dbContext, _sieveProcessor);

    public IBaseRepository<Profile> ProfileRepository =>
        _profileRepository ??= new BaseRepository<Profile>(_dbContext, _sieveProcessor);

    public IBaseRepository<ProfileImage> ProfileImageRepository =>
        _profileImageRepository ??= new BaseRepository<ProfileImage>(_dbContext, _sieveProcessor);

    public IBaseRepository<Question> QuestionRepository =>
        _questionRepository ??= new BaseRepository<Question>(_dbContext, _sieveProcessor);

    public IBaseRepository<Role> RoleRepository =>
        _roleRepository ??= new BaseRepository<Role>(_dbContext, _sieveProcessor);

    public IBaseRepository<User> UserRepository =>
        _userRepository ??= new BaseRepository<User>(_dbContext, _sieveProcessor);

    public IBaseRepository<UserRole> UserRoleRepository =>
        _userRoleRepository ??= new BaseRepository<UserRole>(_dbContext, _sieveProcessor);

    public async Task<int> CommitAsync(CancellationToken cancellationToken = default) =>
        await _dbContext.SaveChangesAsync(cancellationToken);
}