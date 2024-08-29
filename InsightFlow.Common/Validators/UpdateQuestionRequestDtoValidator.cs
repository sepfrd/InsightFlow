using FluentValidation;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class UpdateQuestionRequestDtoValidator : AbstractValidator<UpdateQuestionRequestDto>
{
    public UpdateQuestionRequestDtoValidator()
    {
        RuleFor(updateQuestionRequestDto => updateQuestionRequestDto.NewTitle)
            .MinimumLength(10)
            .MaximumLength(200);

        RuleFor(updateQuestionRequestDto => updateQuestionRequestDto.NewBody)
            .MinimumLength(20)
            .MaximumLength(2000);
    }
}