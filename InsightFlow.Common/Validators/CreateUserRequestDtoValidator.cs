using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class CreateUserRequestDtoValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserRequestDtoValidator()
    {
        RuleFor(createUserRequestDto => createUserRequestDto.Username).Matches(RegexPatternConstants.UsernameRegexPattern);
        RuleFor(createUserRequestDto => createUserRequestDto.Password).Matches(RegexPatternConstants.PasswordRegexPattern);
        RuleFor(createUserRequestDto => createUserRequestDto.Email).Matches(RegexPatternConstants.EmailRegexPattern);
        RuleFor(createUserRequestDto => createUserRequestDto.FirstName).MinimumLength(3).MaximumLength(100);
        RuleFor(createUserRequestDto => createUserRequestDto.LastName).MaximumLength(100);
        RuleFor(createUserRequestDto => createUserRequestDto.Bio).MaximumLength(500);
    }
}