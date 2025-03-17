namespace InsightFlow.Infrastructure.Common.Dtos.Configurations;

public record JwtOptions
{
    public required string PublicKey { get; set; }

    public required string PrivateKey { get; set; }

    public double TokenExpirationDurationMinutes { get; set; }
}