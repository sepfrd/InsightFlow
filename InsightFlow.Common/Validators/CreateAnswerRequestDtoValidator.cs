using FluentValidation;
using InsightFlow.Common.Dtos.Requests;

namespace InsightFlow.Common.Validators;

public class CreateAnswerRequestDtoValidator : AbstractValidator<CreateAnswerRequestDto>
{
    public CreateAnswerRequestDtoValidator()
    {
        RuleFor(createAnswerRequestDto => createAnswerRequestDto.Body)
            .NotEmpty()
            .MinimumLength(10)
            .MaximumLength(2000);
    }
}