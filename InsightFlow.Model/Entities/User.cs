using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class User : BaseEntityWithGuid
{
    [Sieve(CanSort = true, CanFilter = true)]
    public required string Username { get; set; }

    public required string Password { get; set; }

    [Sieve(CanSort = true, CanFilter = true)]
    public int Score { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int PersonId { get; set; }

    public Person? Person { get; set; }

    public Profile? Profile { get; set; }

    public ICollection<Question>? Questions { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }

    public ICollection<Bookmark>? Bookmarks { get; set; }
}