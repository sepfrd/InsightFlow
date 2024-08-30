namespace InsightFlow.Common.Dtos.Requests;

public class UpdateUserRequestDto
{
    public required string Username { get; init; }

    public required string Email { get; init; }

    public required string FirstName { get; init; }

    public string? LastName { get; init; }

    public string? Bio { get; init; }
}