using InsightFlow.Common.Helpers;
using InsightFlow.DataAccess.EntityConfigurations;
using InsightFlow.Model.Entities;
using InsightFlow.Model.Enums;
using Microsoft.EntityFrameworkCore;
using Person = InsightFlow.Model.Entities.Person;

namespace InsightFlow.DataAccess;

public class InsightFlowDbContext : DbContext
{
    public InsightFlowDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Answer>? Answers { get; init; }

    public DbSet<EntityStateInformation> EntityStateInformation { get; init; }

    public DbSet<Person>? Persons { get; init; }

    public DbSet<Profile>? Profiles { get; init; }

    public DbSet<ProfilePicture>? ProfilePictures { get; init; }

    public DbSet<Question>? Questions { get; init; }

    public DbSet<Role>? Roles { get; init; }

    public DbSet<User>? Users { get; init; }

    public DbSet<UserRole>? UserRoles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AnswerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfilePictureEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());

        modelBuilder.Entity<Answer>().HasData(FakeDataHelper.GetFakeAnswers());
        modelBuilder.Entity<Person>().HasData(FakeDataHelper.GetFakePeople());
        modelBuilder.Entity<Profile>().HasData(FakeDataHelper.GetFakeProfiles());
        modelBuilder.Entity<ProfilePicture>().HasData(FakeDataHelper.GetFakeProfilePictures());
        modelBuilder.Entity<Question>().HasData(FakeDataHelper.GetFakeQuestions());
        modelBuilder.Entity<Role>().HasData(FakeDataHelper.GetFakeRoles());
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
    }
}