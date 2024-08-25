namespace InsightFlow.Common.Dtos;

public class UserDto : BaseDto
{
    public string? Username { get; init; }

    public string? Email { get; set; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }

    public ProfileDto? Profile { get; init; }
}