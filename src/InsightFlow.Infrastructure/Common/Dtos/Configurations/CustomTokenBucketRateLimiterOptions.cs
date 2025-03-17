using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public record CustomTokenBucketRateLimiterOptions(
    int TokenLimit,
    int TokensPerPeriod,
    int QueueLimit,
    double ReplenishmentPeriodSeconds,
    bool AutoReplenishment,
    QueueProcessingOrder QueueProcessingOrder);