using FluentValidation;
using Sieve.Models;

namespace InsightFlow.Common.Validations;

public class SieveModelValidator : AbstractValidator<SieveModel>
{
    public SieveModelValidator()
    {
        RuleFor(x => x.PageSize)
            .LessThanOrEqualTo(100);
    }
}