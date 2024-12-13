using Bogus;
using InsightFlow.Model.Entities;

namespace InsightFlow.UnitTests.Common.FakeDataGenerators;

public class FakeUserGenerator
{
    public static List<User> GenerateFakeUsers(int count)
    {
        var id = 0;

        var userFaker = new Faker<User>()
            .RuleFor(user => user.Id, _ => id++)
            .RuleFor(user => user.Username, faker => faker.Internet.UserName().ToLowerInvariant())
            .RuleFor(user => user.Password, _ => "Correct_p0")
            .RuleFor(user => user.Email, faker => faker.Internet.Email().ToLowerInvariant())
            .RuleFor(user => user.FirstName, faker => faker.Name.FirstName())
            .RuleFor(user => user.LastName, faker => faker.Name.LastName())
            .RuleFor(user => user.Answers, _ => [])
            .RuleFor(user => user.Questions, _ => [])
            .RuleFor(user => user.UserRoles, _ => [])
            .RuleFor(user => user.Profile, _ => new Profile());

        var fakeUsers = userFaker.Generate(count);

        return fakeUsers;
    }
}