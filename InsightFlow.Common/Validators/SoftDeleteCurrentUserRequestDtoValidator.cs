using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class SoftDeleteCurrentUserRequestDtoValidator : AbstractValidator<SoftDeleteCurrentUserRequestDto>
{
    public SoftDeleteCurrentUserRequestDtoValidator()
    {
        RuleFor(softDeleteCurrentUserRequestDto => softDeleteCurrentUserRequestDto.Username)
            .Matches(RegexPatternConstants.UsernameRegexPattern);

        RuleFor(softDeleteCurrentUserRequestDto => softDeleteCurrentUserRequestDto.Password)
            .Matches(RegexPatternConstants.PasswordRegexPattern)
            .Equal(softDeleteCurrentUserRequestDto => softDeleteCurrentUserRequestDto.PasswordConfirmation);
    }
}