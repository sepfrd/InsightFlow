using FluentValidation;
using InsightFlow.Model.Entities;

namespace InsightFlow.Common.Validations;

public class RoleValidator : AbstractValidator<Role>
{
    public RoleValidator() =>
        RuleFor(x => x.Title).NotEmpty().MaximumLength(15);
}