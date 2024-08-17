using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class Profile : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string Bio { get; set; } = string.Empty;

    [Sieve(CanFilter = true, CanSort = true)]
    public required string Email { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int UserId { get; set; }

    public User? User { get; set; }
}