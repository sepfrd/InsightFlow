using FluentValidation;
using Sieve.Models;

namespace InsightFlow.Common.Validators;

public class SieveModelValidator : AbstractValidator<SieveModel>
{
    public SieveModelValidator()
    {
        RuleFor(sieveModel => sieveModel.Page)
            .GreaterThanOrEqualTo(1);

        RuleFor(sieveModel => sieveModel.PageSize)
            .GreaterThanOrEqualTo(10)
            .LessThanOrEqualTo(100);
    }
}