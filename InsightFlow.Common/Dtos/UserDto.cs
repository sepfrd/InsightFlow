namespace InsightFlow.Common.Dtos;

public class UserDto : BaseDto
{
    public string? Username { get; init; }

    public string? Email { get; init; }

    public string? FirstName { get; init; }

    public string? LastName { get; init; }
}