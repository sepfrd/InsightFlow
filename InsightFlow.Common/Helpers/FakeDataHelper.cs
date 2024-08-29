using Bogus;
using InsightFlow.Common.Constants;
using InsightFlow.Model.Entities;

namespace InsightFlow.Common.Helpers;

public static class FakeDataHelper
{
    public static readonly Question FakeQuestion = new()
    {
        Id = 1,
        LastUpdated = DateTime.Now,
        Guid = Guid.NewGuid(),
        Title = "How to do some job?",
        Body = "Please help me with my problem.",
        UserId = 1
    };

    public static readonly Answer FakeAnswer = new()
    {
        Id = 1,
        LastUpdated = DateTime.Now,
        Guid = Guid.NewGuid(),
        Body = "This is how you do that job.",
        QuestionId = 1,
        UserId = 2
    };

    public static readonly User FakeAdmin = new()
    {
        Id = 1,
        Guid = Guid.NewGuid(),
        Username = "sepehr_frd",
        Password = PasswordHelper.HashPassword("Sfr1376."),
        Email = "sepfrd@outlook.com",
        FirstName = "Sepehr",
        LastName = "Foroughi Rad"
    };

    public static readonly User FakeUser = new()
    {
        Id = 2,
        Guid = Guid.NewGuid(),
        Username = "bernard_cool",
        Password = PasswordHelper.HashPassword("BernardCool1997."),
        Email = "bercool@gmail.com",
        FirstName = "Bernard",
        LastName = "Cool"
    };

    private static readonly Profile FakeAdminProfile = new()
    {
        Id = 1,
        UserId = 1,
        Bio = ".NET Developer"
    };

    private static readonly Profile FakeUserProfile = new()
    {
        Id = 2,
        UserId = 2,
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
            .RuleFor(answer => answer.Body, faker => faker.Commerce.ProductDescription())
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
            .RuleFor(profile => profile.Bio, faker => faker.Name.JobTitle());

        for (var i = 0; i < 100; i++)
        {
            fakeProfiles.Add(profileFaker.Generate());
        }

        return fakeProfiles;
    }

    public static List<ProfileImage> GetFakeProfileImages()
    {
        var id = 1;

        var fakeProfileImages = new List<ProfileImage>();

        // var fakeProfileImage = GetFakeImageAsync().Result;

        var profileFaker = new Faker<ProfileImage>()
            .RuleFor(profileImage => profileImage.Id, _ => id)
            .RuleFor(profileImage => profileImage.ProfileId, _ => id++);

        for (var i = 0; i < 100; i++)
        {
            fakeProfileImages.Add(profileFaker.Generate());
        }

        return fakeProfileImages;
    }

    public static List<Question> GetFakeQuestions()
    {
        var id = 2;

        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.Id, _ => id++)
            .RuleFor(question => question.Title, faker => faker.Random.Words(2))
            .RuleFor(question => question.Body, faker => faker.Commerce.ProductDescription())
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
            Name = ApplicationConstants.AdminRoleName,
            Description = "Administrator of the Application"
        },
        new Role
        {
            Id = 2,
            Name = ApplicationConstants.UserRoleName,
            Description = "Basic User of the Application"
        }
    ];

    public static List<User> GetFakeUsers()
    {
        var id = 3;

        var password = PasswordHelper.HashPassword("Correct_p0");

        var userFaker = new Faker<User>()
            .RuleFor(user => user.Id, _ => id++)
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.Password, _ => password)
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
            .RuleFor(user => user.LastName, faker => faker.Name.LastName());

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

    // private static Task<byte[]> GetFakeImageAsync()
    // {
    //     var client = new HttpClient
    //     {
    //         Timeout = TimeSpan.FromSeconds(5)
    //     };
    //
    //     var response = client.GetByteArrayAsync("https://picsum.photos/500.jpg");
    //
    //     return response;
    // }
}