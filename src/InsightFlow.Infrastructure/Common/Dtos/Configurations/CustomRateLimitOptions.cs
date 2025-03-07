using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public class CustomRateLimitOptions
{
    public required CustomFixedWindowRateLimiterOptions FixedWindowRateLimiterOptions { get; init; }

    public required CustomTokenBucketRateLimiterOptions TokenBucketRateLimiterOptions { get; init; }

    public required ConcurrencyLimiterOptions ConcurrencyLimiterOptions { get; init; }
}