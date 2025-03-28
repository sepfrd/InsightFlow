using Microsoft.OpenApi.Models;

namespace InsightFlow.Infrastructure.Common.Configurations;

public class AppOptions
{
    public JwtOptions? JwtOptions { get; set; }

    public PaginationOptions? PaginationOptions { get; set; }

    public CustomRateLimitersOptions? RateLimitersConfiguration { get; set; }

    public OpenApiContact? ContactInformation { get; set; }

    public ApplicationInformation? ApplicationInformation { get; set; }

    public string? SqlServerConnectionString { get; set; }
}