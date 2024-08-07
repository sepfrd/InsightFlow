using RedditMockup.Model.BaseEntities;
using Sieve.Attributes;

namespace RedditMockup.Model.Entities;

public class Profile : BaseEntity
{
    public string Bio { get; set; } = string.Empty;

    [Sieve(CanFilter = true, CanSort = true)]
    public required string Email { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }
}