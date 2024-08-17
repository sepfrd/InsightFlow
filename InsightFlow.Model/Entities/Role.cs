using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class Role : BaseEntityWithGuid
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string? Title { get; set; }

    public ICollection<UserRole>? UserRoles { get; set; }
}