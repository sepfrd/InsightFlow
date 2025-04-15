using InsightFlow.Domain.Common;

namespace InsightFlow.Domain.Entities;

public class ProfileImage : DomainEntity
{
    public byte[]? ImageBytes { get; set; }

    public string? ImageFormat { get; set; }

    public long UserId { get; init; }

    public User? User { get; init; }
}