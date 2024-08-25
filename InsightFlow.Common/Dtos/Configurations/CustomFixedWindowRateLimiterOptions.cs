using System.Threading.RateLimiting;

namespace InsightFlow.Common.Dtos.Configurations;

public class CustomFixedWindowRateLimiterOptions
{
    public int PermitLimit { get; set; }

    public double WindowSeconds { get; set; }

    public int QueueLimit { get; set; }

    public QueueProcessingOrder QueueProcessingOrder { get; set; }

    public bool AutoReplenishment { get; set; }
}