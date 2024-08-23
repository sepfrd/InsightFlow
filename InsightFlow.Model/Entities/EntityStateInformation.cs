using InsightFlow.Model.Enums;

namespace InsightFlow.Model.Entities;

public class EntityStateInformation : BaseEntity
{
    public BaseEntityState StateNumber { get; set; }

    public required string Name { get; set; }

    public required string Description { get; set; }
}