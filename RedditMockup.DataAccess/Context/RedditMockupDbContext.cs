using Bogus;
using Microsoft.EntityFrameworkCore;
using RedditMockup.Common.Constants;
using RedditMockup.Common.Helpers;
using RedditMockup.Model.Entities;
using Person = RedditMockup.Model.Entities.Person;

namespace RedditMockup.DataAccess.Context;

public class RedditMockupDbContext : DbContext
{
    public RedditMockupDbContext(DbContextOptions options) : base(options)
    {
    }

    public DbSet<Answer>? Answers { get; init; }

    public DbSet<Person>? Persons { get; init; }

    public DbSet<Profile>? Profiles { get; init; }

    public DbSet<Question>? Questions { get; init; }

    public DbSet<Role>? Roles { get; init; }

    public DbSet<User>? Users { get; init; }

    public DbSet<UserRole>? UserRoles { get; init; }

    public DbSet<AnswerVote>? AnswerVotes { get; init; }

    public DbSet<QuestionVote>? QuestionVotes { get; init; }

    public DbSet<Bookmark>? Bookmarks { get; init; }

    private static List<Person> GetFakePeople()
    {
        var id = 3;

        var personFaker = new Faker<Person>()
            .RuleFor(person => person.Id, _ => id++)
            .RuleFor(person => person.FirstName, faker => faker.Name.FirstName())
            .RuleFor(person => person.LastName, faker => faker.Name.LastName());

        var fakePeople = new List<Person>();

        fakePeople.AddRange(new List<Person>
        {
            new()
            {
                Id = 1,
                FirstName = "Sepehr",
                LastName = "Foroughi Rad"
            },
            new()
            {
                Id = 2,
                FirstName = "Bernard",
                LastName = "Cool"
            }
        });

        for (var i = 0; i < 100; i++)
        {
            fakePeople.Add(personFaker.Generate());
        }

        return fakePeople;
    }

    private static List<User> GetFakeUsers()
    {
        var id = 3;

        var userFaker = new Faker<User>()
            .RuleFor(user => user.Id, _ => id)
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, faker => faker.Internet.Password())
            .RuleFor(user => user.Score, faker => faker.Random.Number(50))
            .RuleFor(user => user.PersonId, _ => id++);

        var fakeUsers = new List<User>();

        fakeUsers.AddRange(new List<User>
        {
            new()
            {
                Id = 1,
                Username = "sepehr_frd",
                Password = "Sfr1376.".GetHashStringAsync().Result,
                PersonId = 1
            },
            new()
            {
                Id = 2,
                Username = "bernard_cool",
                Password = "BernardCool1997".GetHashStringAsync().Result,
                PersonId = 2
            }
        });

        for (var i = 0; i < 100; i++)
        {
            fakeUsers.Add(userFaker.Generate());
        }

        return fakeUsers;
    }

    private static List<Profile> GetFakeProfiles()
    {
        var id = 3;

        var profilesList = new List<Profile>();

        profilesList.AddRange(new List<Profile>
        {
            new()
            {
                Id = 1,
                UserId = 1,
                Email = "sepfrd@outlook.com",
                Bio = ".NET Developer"
            },
            new()
            {
                Id = 2,
                UserId = 2,
                Email = "bercool@gmail.com",
                Bio = "React Developer"
            }
        });

        var profileFaker = new Faker<Profile>()
                .RuleFor(profile => profile.Id, _ => id)
                .RuleFor(profile => profile.UserId, _ => id++)
                .RuleFor(profile => profile.Email, faker => faker.Internet.Email())
                .RuleFor(profile => profile.Bio, faker => faker.Name.JobTitle());

        for (var i = 0; i < 100; i++)
        {
            profilesList.Add(profileFaker.Generate());
        }

        return profilesList;
    }

    private static List<UserRole> GetFakeUserRoles()
    {
        var userRolesList = new List<UserRole>();

        userRolesList.AddRange(new List<UserRole>
        {
            new()
            {
                Id = 1,
                UserId = 1,
                RoleId = 1
            },
            new()
            {
                Id = 2,
                UserId = 2,
                RoleId = 2
            }
        });

        for (var i = 3; i < 102; i++)
        {
            userRolesList.Add(
                new UserRole
                {
                    Id = i,
                    UserId = i,
                    RoleId = 2
                });
        }

        return userRolesList;
    }

    private static List<Question> GetFakeQuestions()
    {
        var id = 1;

        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.Id, _ => id++)
            .RuleFor(question => question.Title, faker => faker.Random.Words(2))
            .RuleFor(question => question.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(question => question.UserId, faker => faker.Random.Number(1, 100));

        var fakeQuestions = new List<Question>();

        for (var i = 0; i < 100; i++)
        {
            fakeQuestions.Add(questionFaker.Generate());
        }

        return fakeQuestions;
    }

    private static List<Answer> GetFakeAnswers()
    {
        var id = 1;

        var answerFaker = new Faker<Answer>()
            .RuleFor(answer => answer.Id, _ => id++)
            .RuleFor(answer => answer.Title, faker => faker.Random.Words(3))
            .RuleFor(answer => answer.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(answer => answer.UserId, faker => faker.Random.Number(1, 100))
            .RuleFor(answer => answer.QuestionId, 1);

        var fakeAnswers = new List<Answer>();

        for (var i = 0; i < 100; i++)
        {
            fakeAnswers.Add(answerFaker.Generate());
        }

        return fakeAnswers;
    }

    private static List<AnswerVote> GetFakeAnswerVotes()
    {
        var id = 1;

        var answerVoteFaker = new Faker<AnswerVote>()
            .RuleFor(answerVote => answerVote.Id, _ => id)
            .RuleFor(answerVote => answerVote.AnswerId, _ => id++)
            .RuleFor(answerVote => answerVote.Kind, faker => faker.Random.Bool());

        var fakeAnswerVotes = new List<AnswerVote>();

        for (var i = 0; i < 100; i++)
        {
            fakeAnswerVotes.Add(answerVoteFaker.Generate());
        }

        return fakeAnswerVotes;
    }

    private static List<QuestionVote> GetFakeQuestionVotes()
    {
        var id = 1;

        var questionVoteFaker = new Faker<QuestionVote>()
            .RuleFor(questionVote => questionVote.Id, _ => id)
            .RuleFor(questionVote => questionVote.QuestionId, _ => id++)
            .RuleFor(questionVote => questionVote.Kind, faker => faker.Random.Bool());

        var fakeQuestionVotes = new List<QuestionVote>();

        for (var i = 0; i < 100; i++)
        {
            fakeQuestionVotes.Add(questionVoteFaker.Generate());
        }

        return fakeQuestionVotes;
    }

    private static List<Bookmark> GetFakeBookmarks()
    {
        var id = 1;

        var bookmarkFaker = new Faker<Bookmark>()
            .RuleFor(bookmark => bookmark.Id, _ => id)
            .RuleFor(bookmark => bookmark.UserId, _ => id)
            .RuleFor(bookmark => bookmark.QuestionId, _ => id++)
            .RuleFor(bookmark => bookmark.IsBookmarked, faker => faker.Random.Bool());

        var fakeBookmarks = new List<Bookmark>();

        for (var i = 0; i < 50; i++)
        {
            fakeBookmarks.Add(bookmarkFaker.Generate());
        }

        return fakeBookmarks;
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<User>().HasIndex(x => x.Username).IsUnique();

        modelBuilder.Entity<User>()
            .HasOne<Person>(user => user.Person)
            .WithOne(person => person.User)
            .HasForeignKey<User>(user => user.PersonId);

        modelBuilder.Entity<Profile>()
            .HasOne<User>(profile => profile.User)
            .WithOne(user => user.Profile)
            .HasForeignKey<Profile>(profile => profile.UserId);

        modelBuilder.Entity<Question>()
            .HasOne<User>(question => question.User)
            .WithMany(user => user.Questions)
            .HasForeignKey(question => question.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Answer>()
            .HasOne<User>(answer => answer.User)
            .WithMany(user => user.Answers)
            .HasForeignKey(answer => answer.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<UserRole>()
            .HasOne<User>(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(userRole => userRole.UserId);

        modelBuilder.Entity<Bookmark>()
            .HasOne<User>(bookmark => bookmark.User)
            .WithMany(user => user.Bookmarks)
            .HasForeignKey(bookmark => bookmark.UserId);

        modelBuilder.Entity<Bookmark>()
            .HasOne<Question>(bookmark => bookmark.Question)
            .WithMany(question => question.Bookmarks)
            .HasForeignKey(bookmark => bookmark.QuestionId);

        modelBuilder.Entity<Answer>()
            .HasOne<Question>(answer => answer.Question)
            .WithMany(question => question.Answers)
            .HasForeignKey(answer => answer.QuestionId);

        modelBuilder.Entity<QuestionVote>()
            .HasOne<Question>(vote => vote.Question)
            .WithMany(question => question.Votes)
            .HasForeignKey(vote => vote.QuestionId);

        modelBuilder.Entity<AnswerVote>()
            .HasOne<Answer>(vote => vote.Answer)
            .WithMany(answer => answer.Votes)
            .HasForeignKey(vote => vote.AnswerId);

        modelBuilder.Entity<UserRole>()
            .HasOne<Role>(userRole => userRole.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(userRole => userRole.RoleId);

        modelBuilder.Entity<Role>().HasData(new List<Role>
        {
            new()
            {
                Id = 1,
                Title = ApplicationConstants.AdminRoleName
            },
            new()
            {
                Id = 2,
                Title = ApplicationConstants.UserRoleName
            }
        });

        modelBuilder.Entity<Person>().HasData(GetFakePeople());

        modelBuilder.Entity<User>().HasData(GetFakeUsers());

        modelBuilder.Entity<Profile>().HasData(GetFakeProfiles());

        modelBuilder.Entity<UserRole>().HasData(GetFakeUserRoles());

        modelBuilder.Entity<AnswerVote>().HasData(GetFakeAnswerVotes());

        modelBuilder.Entity<QuestionVote>().HasData(GetFakeQuestionVotes());

        modelBuilder.Entity<Question>().HasData(GetFakeQuestions());

        modelBuilder.Entity<Answer>().HasData(GetFakeAnswers());

        modelBuilder.Entity<Bookmark>().HasData(GetFakeBookmarks());
    }
}