namespace InsightFlow.Model.Entities;

public class Role : BaseEntity
{
    public required string Title { get; set; }

    public ICollection<UserRole> UserRoles { get; set; } = [];
}