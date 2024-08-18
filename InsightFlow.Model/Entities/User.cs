using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class User : BaseEntityWithGuid
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public int Score { get; set; }

    public int PersonId { get; set; }

    public Person? Person { get; set; }

    public Profile? Profile { get; set; }

    public ICollection<Question>? Questions { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }

    public ICollection<Bookmark>? Bookmarks { get; set; }
}