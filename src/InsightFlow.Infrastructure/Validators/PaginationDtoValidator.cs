using FluentValidation;
using InsightFlow.Infrastructure.Common.Configurations;
using InsightFlow.Infrastructure.Common.Dtos;
using Microsoft.Extensions.Options;

namespace InsightFlow.Infrastructure.Validators;

public class PaginationDtoValidator : AbstractValidator<PaginationDto>
{
    public PaginationDtoValidator(IOptions<AppOptions> appOptions)
    {
        RuleFor(paginationDto => paginationDto.PageNumber)
            .GreaterThanOrEqualTo(1U);

        RuleFor(paginationDto => paginationDto.PageSize)
            .GreaterThanOrEqualTo(appOptions.Value.PaginationOptions!.MinimumAllowedPageSize)
            .LessThanOrEqualTo(appOptions.Value.PaginationOptions.MaximumAllowedPageSize);
    }
}