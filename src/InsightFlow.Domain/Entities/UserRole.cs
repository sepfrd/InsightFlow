using InsightFlow.Domain.Common;

namespace InsightFlow.Domain.Entities;

public class UserRole : DomainEntity
{
    public User? User { get; set; }
    public long UserId { get; set; }
    public Role? Role { get; set; }
    public long RoleId { get; set; }
}