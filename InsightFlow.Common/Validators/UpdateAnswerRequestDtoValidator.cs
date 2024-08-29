using FluentValidation;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class UpdateAnswerRequestDtoValidator : AbstractValidator<UpdateAnswerRequestDto>
{
    public UpdateAnswerRequestDtoValidator()
    {
        RuleFor(updateAnswerRequestDto => updateAnswerRequestDto.NewBody)
            .MinimumLength(20)
            .MaximumLength(2000);
    }
}