using RedditMockup.Model.Entities;

namespace RedditMockup.Common.Helpers;

public class FakeDataHelper
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

    public static readonly Profile FakeAdminProfile = new()
    {
        Id = 1,
        UserId = 1,
        Email = "sepfrd@outlook.com",
        Bio = ".NET Developer"
    };

    public static readonly Profile FakeUserProfile = new()
    {
        Id = 2,
        UserId = 2,
        Email = "bercool@gmail.com",
        Bio = "React Developer"
    };

    public static readonly UserRole FakeAdminUserRole = new()
    {
        Id = 1,
        UserId = 1,
        RoleId = 1
    };

    public static readonly UserRole FakeUserUserRole = new()
    {
        Id = 2,
        UserId = 2,
        RoleId = 2
    };
}