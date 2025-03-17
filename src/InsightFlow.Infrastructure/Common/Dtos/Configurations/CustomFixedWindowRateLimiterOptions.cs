using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public record CustomFixedWindowRateLimiterOptions(
    int PermitLimit,
    int QueueLimit,
    double WindowSeconds,
    bool AutoReplenishment,
    QueueProcessingOrder QueueProcessingOrder);