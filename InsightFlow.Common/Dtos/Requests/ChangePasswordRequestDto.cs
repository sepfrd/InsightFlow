namespace InsightFlow.Common.Dtos.Requests;

public class ChangePasswordRequestDto
{
    public required string OldPassword { get; set; }

    public required string NewPassword { get; set; }

    public required string NewPasswordConfirmation { get; set; }
}