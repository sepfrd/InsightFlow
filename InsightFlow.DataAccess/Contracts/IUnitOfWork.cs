using InsightFlow.Model.Entities;

namespace InsightFlow.DataAccess.Contracts;

public interface IUnitOfWork
{
    IBaseRepository<Answer> AnswerRepository { get; }

    IBaseRepository<EntityStateInformation> EntityStateInformationRepository { get; }

    IBaseRepository<Person> PersonRepository { get; }

    IBaseRepository<Profile> ProfileRepository { get; }

    IBaseRepository<ProfilePicture> ProfilePictureRepository { get; }

    IBaseRepository<Question> QuestionRepository { get; }

    IBaseRepository<Role> RoleRepository { get; }

    IBaseRepository<User> UserRepository { get; }

    IBaseRepository<UserRole> UserRoleRepository { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken);
}