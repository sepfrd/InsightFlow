using InsightFlow.Model.Enums;

namespace InsightFlow.Model.Entities;

public class BaseEntity
{
    protected BaseEntity()
    {
        CreationDate = LastUpdated = DateTime.Now;
        Guid = Guid.NewGuid();
    }

    public int Id { get; init; }

    public Guid Guid { get; init; }

    public DateTime CreationDate { get; private init; }

    public DateTime LastUpdated { get; set; }

    public BaseEntityState State { get; set; } = BaseEntityState.Active;
}