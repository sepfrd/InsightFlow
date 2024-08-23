using FluentValidation;
using InsightFlow.Common.Dtos;

namespace InsightFlow.Common.Validations;

public class QuestionDtoValidator : AbstractValidator<QuestionDto>
{
    public QuestionDtoValidator()
    {
        RuleFor(questionDto => questionDto.Title)
            .MinimumLength(10)
            .MaximumLength(200);

        RuleFor(questionDto => questionDto.Body)
            .MinimumLength(20)
            .MaximumLength(2000);
    }
}