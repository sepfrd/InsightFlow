namespace InsightFlow.Common.Dtos.Requests;

public class SoftDeleteCurrentUserRequestDto
{
    public required string Username { get; set; }

    public required string Password { get; set; }

    public required string PasswordConfirmation { get; set; }
}