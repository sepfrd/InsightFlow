using InsightFlow.Domain.Common;
using InsightFlow.Domain.Enums;

namespace InsightFlow.Domain.Entities;

public class User : DomainEntity
{
    public required string Username { get; set; }
    public required string PasswordHash { get; set; }
    public required string Email { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public ICollection<UserRole> UserRoles { get; set; } = [];
    public ICollection<BlogPost> BlogPosts { get; set; } = [];
    public ICollection<Comment> Comments { get; set; } = [];
    public ICollection<Reaction> Reactions { get; set; } = [];
}