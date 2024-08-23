using FluentValidation;
using InsightFlow.Common.Constants;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.Username).Matches(RegexPatternConstants.UsernamePattern);
        RuleFor(loginDto => loginDto.Password).Matches(RegexPatternConstants.PasswordPattern);
    }
}