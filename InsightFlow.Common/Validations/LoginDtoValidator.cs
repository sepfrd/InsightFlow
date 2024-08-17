using FluentValidation;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(x => x.Username).NotEmpty().MaximumLength(20).MinimumLength(8);

        RuleFor(x => x.Password).NotEmpty().MinimumLength(6);
    }
}