using InsightFlow.Common.Helpers;
using InsightFlow.DataAccess.EntityConfigurations;
using InsightFlow.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Person = InsightFlow.Model.Entities.Person;

namespace InsightFlow.DataAccess;

public class InsightFlowDbContext : DbContext
{
    public InsightFlowDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Answer>? Answers { get; init; }

    public DbSet<AnswerVote>? AnswerVotes { get; init; }

    public DbSet<Bookmark>? Bookmarks { get; init; }

    public DbSet<Person>? Persons { get; init; }

    public DbSet<Profile>? Profiles { get; init; }

    public DbSet<Question>? Questions { get; init; }

    public DbSet<QuestionVote>? QuestionVotes { get; init; }

    public DbSet<Role>? Roles { get; init; }

    public DbSet<User>? Users { get; init; }

    public DbSet<UserRole>? UserRoles { get; init; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AnswerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerVoteEntityConfiguration());
        modelBuilder.ApplyConfiguration(new BookmarkEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionVoteEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());

        modelBuilder.Entity<Answer>().HasData(FakeDataHelper.GetFakeAnswers());
        modelBuilder.Entity<AnswerVote>().HasData(FakeDataHelper.GetFakeAnswerVotes());
        modelBuilder.Entity<Bookmark>().HasData(FakeDataHelper.GetFakeBookmarks());
        modelBuilder.Entity<Person>().HasData(FakeDataHelper.GetFakePeople());
        modelBuilder.Entity<Profile>().HasData(FakeDataHelper.GetFakeProfiles());
        modelBuilder.Entity<Question>().HasData(FakeDataHelper.GetFakeQuestions());
        modelBuilder.Entity<QuestionVote>().HasData(FakeDataHelper.GetFakeQuestionVotes());
        modelBuilder.Entity<Role>().HasData(FakeDataHelper.GetFakeRoles());
        modelBuilder.Entity<User>().HasData(FakeDataHelper.GetFakeUsers());
        modelBuilder.Entity<UserRole>().HasData(FakeDataHelper.GetFakeUserRoles());
    }
}