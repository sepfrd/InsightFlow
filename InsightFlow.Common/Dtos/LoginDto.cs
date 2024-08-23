namespace InsightFlow.Common.Dtos;

public class LoginDto
{
    public required string Username { get; init; }

    public required string Password { get; init; }

    public bool RememberMe { get; init; }

}