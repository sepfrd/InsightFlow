using FluentValidation;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class ProfileDtoValidator : AbstractValidator<ProfileDto>
{
    public ProfileDtoValidator()
    {
        RuleFor(x => x.Bio).MaximumLength(40);

        RuleFor(x => x.Email).EmailAddress();
    }
}