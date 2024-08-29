using InsightFlow.Model.Enums;

namespace InsightFlow.Model.Entities;

public class EntityStateInformation : BaseEntity
{
    public BaseEntityState StateNumber { get; init; }

    public required string Name { get; init; }

    public required string Description { get; init; }
}