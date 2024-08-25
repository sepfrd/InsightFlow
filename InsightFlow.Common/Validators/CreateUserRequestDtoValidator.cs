using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class CreateUserRequestDtoValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserRequestDtoValidator()
    {
        RuleFor(requestDto => requestDto.Username).Matches(RegexPatternConstants.UsernameRegexPattern);
        RuleFor(requestDto => requestDto.Password).Matches(RegexPatternConstants.PasswordRegexPattern);
        RuleFor(requestDto => requestDto.Email).Matches(RegexPatternConstants.EmailRegexPattern);
        RuleFor(requestDto => requestDto.FirstName).MinimumLength(3).MaximumLength(100);
        RuleFor(requestDto => requestDto.LastName).MaximumLength(100);
        RuleFor(requestDto => requestDto.Bio).MaximumLength(500);
    }
}