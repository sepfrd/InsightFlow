namespace InsightFlow.Common.Dtos;

public class LoginDto
{
    public required string UsernameOrEmail { get; init; }

    public required string Password { get; init; }
}