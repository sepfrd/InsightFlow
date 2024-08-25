using FluentValidation;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class CreateQuestionRequestDtoValidator : AbstractValidator<CreateQuestionRequestDto>
{
    public CreateQuestionRequestDtoValidator()
    {
        RuleFor(requestDto => requestDto.Title)
            .MinimumLength(10)
            .MaximumLength(200);

        RuleFor(requestDto => requestDto.Body)
            .MinimumLength(20)
            .MaximumLength(2000);
    }
}