namespace InsightFlow.Model.Entities;

public class Role : BaseEntity
{
    public required string Name { get; set; }

    public required string Description { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
}