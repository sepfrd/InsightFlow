using FluentValidation;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class ProfileDtoValidator : AbstractValidator<ProfileDto>
{
    public ProfileDtoValidator()
    {
        RuleFor(profileDto => profileDto.Bio).MaximumLength(500);
    }
}