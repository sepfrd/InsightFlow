using FluentValidation;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class UpdateQuestionRequestDtoValidator : AbstractValidator<UpdateQuestionRequestDto>
{
    public UpdateQuestionRequestDtoValidator()
    {
        RuleFor(questionDto => questionDto.NewTitle)
            .MinimumLength(10)
            .MaximumLength(200);

        RuleFor(questionDto => questionDto.NewBody)
            .MinimumLength(20)
            .MaximumLength(2000);
    }
}