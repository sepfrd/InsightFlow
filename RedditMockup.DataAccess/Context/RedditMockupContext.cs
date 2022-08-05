using Bogus;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Dtos;
using RedditMockup.Common.Helpers;
using RedditMockup.Model.Entities;
using Person = RedditMockup.Model.Entities.Person;

namespace RedditMockup.DataAccess.Context;

public class RedditMockupContext : DbContext
{
    #region [Constructor]

    public RedditMockupContext(DbContextOptions options) : base(options)
    {
    }

    #endregion

    #region [Properties]

    public DbSet<Answer>? Answers { get; set; }

    public DbSet<Person>? Persons { get; set; }

    public DbSet<Profile>? Profiles { get; set; }

    public DbSet<Question>? Questions { get; set; }

    public DbSet<Role>? Roles { get; set; }

    public DbSet<User>? Users { get; set; }

    public DbSet<UserRole>? UserRoles { get; set; }

    public DbSet<AnswerVote>? Votes { get; set; }

    #endregion

    #region [Methods]

    private static IEnumerable<Question> GetFakeQuestions()
    {
        var id = 1;
        
        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.Id, faker => id++)
            .RuleFor(question => question.Title, faker => faker.Lorem.Letter(8))
            .RuleFor(question => question.Description, faker => faker.Lorem.Letter(30))
            .RuleFor(question => question.UserId, faker => faker.Random.Number(1, 2));

        var fakeQuestions = new List<Question>();

        for (var i = 0; i < 10; i++)
        {
            fakeQuestions.Add(questionFaker.Generate());
        }

        return fakeQuestions;
    }
    
    private static IEnumerable<Answer> GetFakeAnswers()
    {
        int id = 1;
        
        var answerFaker = new Faker<Answer>()
            .RuleFor(answer => answer.Id, faker => id++)
            .RuleFor(answer => answer.Title, faker => faker.Lorem.Letter(8))
            .RuleFor(answer => answer.Description, faker => faker.Lorem.Letter(30))
            .RuleFor(answer => answer.UserId, faker => faker.Random.Number(1, 2))
            .RuleFor(answer => answer.QuestionId, faker => faker.Random.Number(1, 10));

        var fakeAnswers = new List<Answer>();

        for (var i = 0; i < 10; i++)
        {
            fakeAnswers.Add(answerFaker.Generate());
        }

        return fakeAnswers;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Role>().HasData(new List<Role>
        {
            new() { Id = 1, Title = RoleConstants.Admin, }, new() { Id = 2, Title = RoleConstants.User, }
        });

        modelBuilder.Entity<Person>().HasData(new List<Person>
        {
            new() { Id = 1, Name = "Mahyar", Family = "Hoorbakht" },
            new() { Id = 2, Name = "Sepehr", Family = "Foroughi Rad" }
        });

        modelBuilder.Entity<User>().HasData(new List<User>
        {
            new()
            {
                Id = 1,
                Username = "admin_admin",
                Password = "adminnnn".GetHashStringAsync().Result,
                PersonId = 1
            },
            new()
            {
                Id = 2, Username = "sepehr_frd", Password = "sfr1376".GetHashStringAsync().Result, PersonId = 2
            }
        });

        modelBuilder.Entity<Profile>()
            .HasData(new List<Profile> { new() { Id = 1, UserId = 1, }, new() { Id = 2, UserId = 2, } });

        modelBuilder.Entity<UserRole>().HasData(new List<UserRole>
        {
            new() { Id = 1, UserId = 1, RoleId = 1 }, new() { Id = 2, UserId = 2, RoleId = 2 }
        });

        modelBuilder.Entity<Question>().HasData(GetFakeQuestions());
        
        modelBuilder.Entity<Answer>().HasData(GetFakeAnswers());
    }

    #endregion
}