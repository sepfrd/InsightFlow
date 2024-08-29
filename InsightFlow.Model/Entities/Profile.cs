namespace InsightFlow.Model.Entities;

public class Profile : BaseEntity
{
    public string Bio { get; set; } = string.Empty;

    public ProfileImage? ProfileImage { get; init; }

    public int UserId { get; init; }

    public User? User { get; init; }
}