using InsightFlow.Domain.Common;

namespace InsightFlow.Domain.Entities;

public class Role : DomainEntity
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
}