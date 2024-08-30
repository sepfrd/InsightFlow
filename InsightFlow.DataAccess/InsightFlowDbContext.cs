using InsightFlow.Common.Constants;
using InsightFlow.Common.Helpers;
using InsightFlow.DataAccess.EntityConfigurations;
using InsightFlow.Model.Entities;
using InsightFlow.Model.Enums;
using Microsoft.EntityFrameworkCore;

namespace InsightFlow.DataAccess;

public class InsightFlowDbContext : DbContext
{
    public InsightFlowDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Answer>? Answers { get; init; }

    public DbSet<EntityStateInformation> EntityStateInformation { get; init; }

    public DbSet<Profile>? Profiles { get; init; }

    public DbSet<ProfileImage>? ProfileImages { get; init; }

    public DbSet<Question>? Questions { get; init; }

    public DbSet<Role>? Roles { get; init; }

    public DbSet<User>? Users { get; init; }

    public DbSet<UserRole>? UserRoles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AnswerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileImageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());

        modelBuilder.Entity<Answer>().HasData(FakeDataHelper.GetFakeAnswers());
        modelBuilder.Entity<Profile>().HasData(FakeDataHelper.GetFakeProfiles());
        modelBuilder.Entity<ProfileImage>().HasData(FakeDataHelper.GetFakeProfileImages());
        modelBuilder.Entity<Question>().HasData(FakeDataHelper.GetFakeQuestions());
        modelBuilder.Entity<User>().HasData(FakeDataHelper.GetFakeUsers());
        modelBuilder.Entity<UserRole>().HasData(FakeDataHelper.GetFakeUserRoles());

        modelBuilder.Entity<EntityStateInformation>().HasData([
            new EntityStateInformation
            {
                Id = 1,
                StateNumber = BaseEntityState.Active,
                Name = nameof(BaseEntityState.Active),
                Description = "The entity is active."
            },
            new EntityStateInformation
            {
                Id = 2,
                StateNumber = BaseEntityState.Inactive,
                Name = nameof(BaseEntityState.Inactive),
                Description = "The entity is inactive."
            },
            new EntityStateInformation
            {
                Id = 3,
                StateNumber = BaseEntityState.Deleted,
                Name = nameof(BaseEntityState.Deleted),
                Description = "The entity is (soft) deleted."
            }
        ]);

        modelBuilder.Entity<Role>().HasData(
        [
            new Role
            {
                Id = ApplicationConstants.AdminRoleId,
                Name = ApplicationConstants.AdminRoleName,
                Description = "Administrator of the Application"
            },
            new Role
            {
                Id = ApplicationConstants.UserRoleId,
                Name = ApplicationConstants.UserRoleName,
                Description = "Basic User of the Application"
            }
        ]);
    }
}