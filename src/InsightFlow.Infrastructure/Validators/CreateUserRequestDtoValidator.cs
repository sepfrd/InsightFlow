using FluentValidation;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Dtos;

namespace InsightFlow.Infrastructure.Validators;

public class CreateUserRequestDtoValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserRequestDtoValidator()
    {
        RuleFor(createUserCommand => createUserCommand.Email)
            .Matches(DomainConstants.EmailRegexPattern);

        RuleFor(createUserCommand => createUserCommand.Username)
            .Matches(DomainConstants.UsernameRegexPattern);

        RuleFor(createUserCommand => createUserCommand.Password)
            .Matches(DomainConstants.PasswordRegexPattern);
    }
}