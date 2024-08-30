using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class ChangePasswordRequestDtoValidator : AbstractValidator<ChangePasswordRequestDto>
{
    public ChangePasswordRequestDtoValidator()
    {
        RuleFor(changePasswordRequestDto => changePasswordRequestDto.OldPassword)
            .Matches(RegexPatternConstants.PasswordRegexPattern);

        RuleFor(changePasswordRequestDto => changePasswordRequestDto.NewPassword)
            .Matches(RegexPatternConstants.PasswordRegexPattern)
            .Equal(changePasswordRequestDto => changePasswordRequestDto.NewPasswordConfirmation);
    }
}