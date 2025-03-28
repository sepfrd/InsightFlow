using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Configurations;

public class CustomRateLimitersOptions
{
    public CustomFixedWindowRateLimiterOptions? FixedWindowRateLimiterOptions { get; set; }

    public CustomTokenBucketRateLimiterOptions? TokenBucketRateLimiterOptions { get; set; }

    public ConcurrencyLimiterOptions? ConcurrencyLimiterOptions { get; set; }
}