using InsightFlow.DataAccess.EntityConfigurations;
using InsightFlow.Model.Entities;
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

        modelBuilder.ApplyConfiguration(new BaseEntityConfiguration());
        modelBuilder.ApplyConfiguration(new AnswerEntityConfiguration());
        modelBuilder.ApplyConfiguration(new EntityStateInformationEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileEntityConfiguration());
        modelBuilder.ApplyConfiguration(new ProfileImageEntityConfiguration());
        modelBuilder.ApplyConfiguration(new QuestionEntityConfiguration());
        modelBuilder.ApplyConfiguration(new RoleEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserEntityConfiguration());
        modelBuilder.ApplyConfiguration(new UserRoleEntityConfiguration());
    }
}