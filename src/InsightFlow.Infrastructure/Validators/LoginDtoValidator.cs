using FluentValidation;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Dtos;

namespace InsightFlow.Infrastructure.Validators;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.UsernameOrEmail)
            .Matches($"{DomainConstants.UsernameRegexPattern}|{DomainConstants.EmailRegexPattern}");

        RuleFor(createUserRequestDto => createUserRequestDto.Password)
            .Matches(DomainConstants.PasswordRegexPattern);
    }
}