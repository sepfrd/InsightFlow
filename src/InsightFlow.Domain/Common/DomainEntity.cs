namespace InsightFlow.Domain.Common;

public abstract class DomainEntity
{
    protected DomainEntity()
    {
        Uuid = Guid.CreateVersion7();
        CreatedAt = UpdatedAt = DateTime.UtcNow;
    }

    public long Id { get; set; }

    public Guid Uuid { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public void PrepareForUpdate() => UpdatedAt = DateTime.UtcNow;
}