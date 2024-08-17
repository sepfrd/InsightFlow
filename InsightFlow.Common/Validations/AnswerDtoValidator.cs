using FluentValidation;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class AnswerDtoValidator : AbstractValidator<AnswerDto>
{
    public AnswerDtoValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MinimumLength(5);
        RuleFor(x => x.Description).NotEmpty().MinimumLength(5);
    }
}