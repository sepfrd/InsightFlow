using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.UsernameOrEmail).Matches(RegexPatternConstants.UsernameOrEmailRegexPattern);
        RuleFor(loginDto => loginDto.Password).Matches(RegexPatternConstants.PasswordRegexPattern);
    }
}