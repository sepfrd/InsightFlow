using System.Threading.RateLimiting;

namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public class CustomTokenBucketRateLimiterOptions
{
    public int TokenLimit { get; set; }

    public int TokensPerPeriod { get; set; }

    public int QueueLimit { get; set; }

    public double ReplenishmentPeriodSeconds { get; set; }

    public bool AutoReplenishment { get; set; }

    public QueueProcessingOrder QueueProcessingOrder { get; set; }
}