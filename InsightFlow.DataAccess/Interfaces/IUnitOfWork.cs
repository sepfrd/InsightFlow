using InsightFlow.Model.Entities;

namespace InsightFlow.DataAccess.Interfaces;

public interface IUnitOfWork
{
    IBaseRepository<Answer> AnswerRepository { get; }

    IBaseRepository<EntityStateInformation> EntityStateInformationRepository { get; }

    IBaseRepository<Profile> ProfileRepository { get; }

    IBaseRepository<ProfileImage> ProfileImageRepository { get; }

    IBaseRepository<Question> QuestionRepository { get; }

    IBaseRepository<Role> RoleRepository { get; }

    IBaseRepository<User> UserRepository { get; }

    IBaseRepository<UserRole> UserRoleRepository { get; }

    Task<int> CommitAsync(CancellationToken cancellationToken = default);
}