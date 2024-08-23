namespace InsightFlow.Model.Entities;

public class Profile : BaseEntity
{
    public string Bio { get; set; } = string.Empty;

    public required string Email { get; set; }

    public ProfilePicture? ProfilePicture { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }
}