using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public record CustomRateLimitOptions(
    CustomFixedWindowRateLimiterOptions FixedWindowRateLimiterOptions,
    CustomTokenBucketRateLimiterOptions TokenBucketRateLimiterOptions,
    ConcurrencyLimiterOptions ConcurrencyLimiterOptions);