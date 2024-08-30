using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class UpdateUserRequestDtoValidator : AbstractValidator<UpdateUserRequestDto>
{
    public UpdateUserRequestDtoValidator()
    {
        RuleFor(updateUserRequestDto => updateUserRequestDto.Username)
            .Matches(RegexPatternConstants.UsernameRegexPattern);

        RuleFor(updateUserRequestDto => updateUserRequestDto.Email)
            .Matches(RegexPatternConstants.EmailRegexPattern);

        RuleFor(updateUserRequestDto => updateUserRequestDto.FirstName)
            .MinimumLength(3)
            .MaximumLength(100);

        RuleFor(updateUserRequestDto => updateUserRequestDto.LastName)
            .MaximumLength(100);

        RuleFor(updateUserRequestDto => updateUserRequestDto.Bio)
            .MaximumLength(500);
    }
}