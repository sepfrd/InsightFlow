using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public class CustomRateLimitOptions
{
    public CustomFixedWindowRateLimiterOptions? FixedWindowRateLimiterOptions { get; set; }

    public CustomTokenBucketRateLimiterOptions? TokenBucketRateLimiterOptions { get; set; }

    public ConcurrencyLimiterOptions? ConcurrencyLimiterOptions { get; set; }
}