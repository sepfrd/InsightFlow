using FluentValidation;
using InsightFlow.Model.Entities;

namespace InsightFlow.Common.Validations;

public class PersonValidator : AbstractValidator<Person>
{
    public PersonValidator()
    {
        RuleFor(x => x.FirstName).NotEmpty().MaximumLength(20);
        RuleFor(x => x.LastName).NotEmpty().MaximumLength(20);
    }
}