using Bogus;
using InsightFlow.Common.Constants;
using InsightFlow.Model.Entities;

namespace InsightFlow.Common.Helpers;

public static class FakeDataHelper
{
    public static readonly User FakeAdmin = new()
    {
        Guid = Guid.NewGuid(),
        Username = "sepehr_frd",
        Password = PasswordHelper.HashPassword("Sfr1376."),
        Email = "sepfrd@outlook.com",
        FirstName = "Sepehr",
        LastName = "Foroughi Rad"
    };

    public static readonly User FakeUser = new()
    {
        Guid = Guid.NewGuid(),
        Username = "bernard_cool",
        Password = PasswordHelper.HashPassword("BernardCool1997."),
        Email = "bercool@gmail.com",
        FirstName = "Bernard",
        LastName = "Cool"
    };

    public static List<Answer> GetFakeAnswers(List<Question> fakeQuestions, List<User> fakeUsers, int countToGenerate)
    {
        var answerFaker = new Faker<Answer>()
            .RuleFor(answer => answer.Body, faker => faker.Commerce.ProductDescription())
            .RuleFor(answer => answer.User, faker => faker.PickRandom(fakeUsers))
            .RuleFor(answer => answer.Question, faker => faker.PickRandom(fakeQuestions));

        var fakeAnswers = answerFaker.Generate(countToGenerate);

        return fakeAnswers;
    }

    public static List<Profile> GetFakeProfiles(List<User> fakeUsers)
    {
        var fakeProfiles = new List<Profile>
        {
            new()
            {
                User = fakeUsers.First(user => user.Username == FakeAdmin.Username),
                Bio = ".NET Developer"
            },
            new()
            {
                User = fakeUsers.First(user => user.Username == FakeUser.Username),
                Bio = "React Developer"
            }
        };

        var currentUserProfilesCount = fakeProfiles.Count;

        var fakeUsersIndex = currentUserProfilesCount;

        var profileFaker = new Faker<Profile>()
            .RuleFor(profile => profile.User, _ => fakeUsers[fakeUsersIndex++])
            .RuleFor(profile => profile.Bio, faker => faker.Name.JobTitle());

        fakeProfiles.AddRange(profileFaker.Generate(fakeUsers.Count - currentUserProfilesCount));

        return fakeProfiles;
    }

    public static List<ProfileImage> GetFakeProfileImages(List<Profile> fakeProfiles)
    {
        var fakeProfilesIndex = 0;

        // var fakeProfileImage = GetFakeImageAsync().Result;

        var profileFaker = new Faker<ProfileImage>()
            .RuleFor(profileImage => profileImage.Profile, _ => fakeProfiles[fakeProfilesIndex++]);

        var fakeProfileImages = profileFaker.Generate(fakeProfiles.Count);

        return fakeProfileImages;
    }

    public static List<Question> GetFakeQuestions(List<User> fakeUsers, int countToGenerate)
    {
        var questionFaker = new Faker<Question>()
            .RuleFor(question => question.Title, faker => faker.Random.Words(2))
            .RuleFor(question => question.Body, faker => faker.Commerce.ProductDescription())
            .RuleFor(question => question.User, faker => faker.PickRandom(fakeUsers));

        var fakeQuestions = questionFaker.Generate(countToGenerate);

        return fakeQuestions;
    }

    public static List<User> GetFakeUsers(int countToGenerate)
    {
        var password = PasswordHelper.HashPassword("Correct_p0");

        var userFaker = new Faker<User>()
            .RuleFor(user => user.Username, faker => faker.Internet.UserName().ToLowerInvariant())
            .RuleFor(user => user.Password, _ => password)
            .RuleFor(user => user.Email, faker => faker.Internet.Email().ToLowerInvariant())
            .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
            .RuleFor(user => user.LastName, faker => faker.Name.LastName());

        var fakeUsers = new List<User>();

        fakeUsers.AddRange(new List<User>
        {
            FakeAdmin,
            FakeUser
        });

        var currentUsersCount = fakeUsers.Count;

        fakeUsers.AddRange(userFaker.Generate(countToGenerate - currentUsersCount));

        return fakeUsers;
    }

    public static List<UserRole> GetFakeUserRoles(List<User> fakeUsers, List<Role> fakeRoles)
    {
        var userRolesList = new List<UserRole>
        {
            new()
            {
                User = fakeUsers.First(user => user.Username == FakeAdmin.Username),
                Role = fakeRoles.First(role => role.Name == ApplicationConstants.AdminRoleName)
            },
            new()
            {
                User = fakeUsers.First(user => user.Username == FakeUser.Username),
                Role = fakeRoles.First(role => role.Name == ApplicationConstants.UserRoleName)
            }
        };

        var currentUserRolesListCount = userRolesList.Count;

        userRolesList.AddRange(fakeUsers[currentUserRolesListCount..]
            .Select(user =>
                new UserRole
                {
                    User = user,
                    Role = fakeRoles[Random.Shared.Next(0, fakeRoles.Count)]
                }));

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