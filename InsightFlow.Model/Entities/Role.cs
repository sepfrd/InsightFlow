namespace InsightFlow.Model.Entities;

public class Role : BaseEntity
{
    public required string Name { get; init; }

    public required string Description { get; init; }

    public ICollection<UserRole> UserRoles { get; init; } = [];
}