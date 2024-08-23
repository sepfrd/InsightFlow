using Bogus;
using InsightFlow.Common.Constants;
using InsightFlow.Model.Entities;
using Person = InsightFlow.Model.Entities.Person;

namespace InsightFlow.Common.Helpers;

public static class FakeDataHelper
{
    public static readonly Question FakeQuestion = new()
    {
        Id = 1,
        LastUpdated = DateTime.Now,
        Guid = Guid.NewGuid(),
        Title = "How to do some job?",
        Description = "Please help me with my problem.",
        UserId = 1
    };

    public static readonly Answer FakeAnswer = new()
    {
        Id = 1,
        LastUpdated = DateTime.Now,
        Guid = Guid.NewGuid(),
        Title = "Answer for doing some job",
        Description = "This is how you do that job.",
        QuestionId = 1,
        UserId = 2
    };

    public static readonly User FakeAdmin = new()
    {
        Id = 1,
        Guid = Guid.NewGuid(),
        Username = "sepehr_frd",
        Password = "Sfr1376.".GetHashStringAsync().Result,
        PersonId = 1
    };

    public static readonly User FakeUser = new()
    {
        Id = 2,
        Guid = Guid.NewGuid(),
        Username = "bernard_cool",
        Password = "BernardCool1997".GetHashStringAsync().Result,
        PersonId = 2
    };

    private static readonly Profile FakeAdminProfile = new()
    {
        Id = 1,
        UserId = 1,
        Email = "sepfrd@outlook.com",
        Bio = ".NET Developer"
    };

    private static readonly Profile FakeUserProfile = new()
    {
        Id = 2,
        UserId = 2,
        Email = "bercool@gmail.com",
        Bio = "React Developer"
    };

    private static readonly UserRole FakeAdminUserRole = new()
    {
        Id = 1,
        UserId = 1,
        RoleId = 1
    };

    private static readonly UserRole FakeUserUserRole = new()
    {
        Id = 2,
        UserId = 2,
        RoleId = 2
    };

    public static List<Answer> GetFakeAnswers()
    {
        var id = 2;

        var answerFaker = new Faker<Answer>()
            .RuleFor(answer => answer.Id, _ => id++)
            .RuleFor(answer => answer.Title, faker => faker.Random.Words(3))
            .RuleFor(answer => answer.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(answer => answer.UserId, faker => faker.Random.Number(1, 100))
            .RuleFor(answer => answer.QuestionId, 1);

        var fakeAnswers = new List<Answer>
        {
            FakeAnswer
        };

        for (var i = 0; i < 100; i++)
        {
            fakeAnswers.Add(answerFaker.Generate());
        }

        return fakeAnswers;
    }

    public static List<Person> GetFakePeople()
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

    public static List<Profile> GetFakeProfiles()
    {
        var id = 3;

        var fakeProfiles = new List<Profile>();

        fakeProfiles.AddRange(new List<Profile>
        {
            FakeAdminProfile,
            FakeUserProfile
        });

        var profileFaker = new Faker<Profile>()
            .RuleFor(profile => profile.Id, _ => id)
            .RuleFor(profile => profile.UserId, _ => id++)
            .RuleFor(profile => profile.Email, faker => faker.Internet.Email())
            .RuleFor(profile => profile.Bio, faker => faker.Name.JobTitle());

        for (var i = 0; i < 100; i++)
        {
            fakeProfiles.Add(profileFaker.Generate());
        }

        return fakeProfiles;
    }

    public static List<ProfilePicture> GetFakeProfilePictures()
    {
        var id = 1;

        var fakeProfilePictures = new List<ProfilePicture>();

        // var fakeProfilePicture = await GetFakeImageAsync();

        var profileFaker = new Faker<ProfilePicture>()
            .RuleFor(profilePicture => profilePicture.Id, _ => id)
            .RuleFor(profilePicture => profilePicture.ProfileId, _ => id++);

        for (var i = 0; i < 100; i++)
        {
            fakeProfilePictures.Add(profileFaker.Generate());
        }

        return fakeProfilePictures;
    }

    public static List<Question> GetFakeQuestions()
    {
        var id = 2;

        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.Id, _ => id++)
            .RuleFor(question => question.Title, faker => faker.Random.Words(2))
            .RuleFor(question => question.Description, faker => faker.Commerce.ProductDescription())
            .RuleFor(question => question.UserId, faker => faker.Random.Number(1, 100));

        var fakeQuestions = new List<Question>
        {
            FakeQuestion
        };

        for (var i = 0; i < 100; i++)
        {
            fakeQuestions.Add(questionFaker.Generate());
        }

        return fakeQuestions;
    }

    public static List<Role> GetFakeRoles() =>
    [
        new Role
        {
            Id = 1,
            Title = ApplicationConstants.AdminRoleName
        },
        new Role
        {
            Id = 2,
            Title = ApplicationConstants.UserRoleName
        }
    ];

    public static List<User> GetFakeUsers()
    {
        var id = 3;

        var userFaker = new Faker<User>()
            .RuleFor(user => user.Id, _ => id)
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, faker => faker.Internet.Password())
            .RuleFor(user => user.PersonId, _ => id++);

        var fakeUsers = new List<User>();

        fakeUsers.AddRange(new List<User>
        {
            FakeAdmin,
            FakeUser
        });

        for (var i = 0; i < 100; i++)
        {
            fakeUsers.Add(userFaker.Generate());
        }

        return fakeUsers;
    }

    public static List<UserRole> GetFakeUserRoles()
    {
        var userRolesList = new List<UserRole>();

        userRolesList.AddRange(new List<UserRole>
        {
            FakeAdminUserRole,
            FakeUserUserRole
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

    // private async static Task<byte[]?> GetFakeImageAsync()
    // {
    //     var client = new HttpClient
    //     {
    //         Timeout = TimeSpan.FromSeconds(5)
    //     };
    //
    //     var response = await client.GetByteArrayAsync("https://picsum.photos/1000");
    //
    //     return response;
    // }
}