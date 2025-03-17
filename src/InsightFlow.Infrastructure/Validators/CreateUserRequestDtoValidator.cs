using FluentValidation;
using InsightFlow.Domain.Common;
using InsightFlow.Infrastructure.Common.Dtos;

namespace InsightFlow.Infrastructure.Validators;

public class CreateUserRequestDtoValidator : AbstractValidator<CreateUserRequestDto>
{
    public CreateUserRequestDtoValidator()
    {
        RuleFor(createUserRequestDto => createUserRequestDto.Email)
            .Matches(DomainConstants.EmailRegexPattern);

        RuleFor(createUserRequestDto => createUserRequestDto.Username)
            .Matches(DomainConstants.UsernameRegexPattern);

        RuleFor(createUserRequestDto => createUserRequestDto.Password)
            .Matches(DomainConstants.PasswordRegexPattern);
    }
}