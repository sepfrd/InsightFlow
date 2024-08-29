namespace InsightFlow.Model.Entities;

public class User : BaseEntity
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string Email { get; set; }

    public required string FirstName { get; set; }

    public string? LastName { get; set; }

    public Profile? Profile { get; init; }

    public ICollection<Question> Questions { get; init; } = [];

    public ICollection<Answer> Answers { get; init; } = [];

    public ICollection<UserRole> UserRoles { get; init; } = [];
}