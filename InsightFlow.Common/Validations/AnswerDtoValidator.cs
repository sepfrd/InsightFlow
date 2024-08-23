using FluentValidation;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class AnswerDtoValidator : AbstractValidator<AnswerDto>
{
    public AnswerDtoValidator()
    {
        RuleFor(answerDto => answerDto.Body)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(2000);
    }
}