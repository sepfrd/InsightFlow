using Microsoft.EntityFrameworkCore;

namespace RedditMockup.Model.BaseEntities;

[Index("Guid", IsUnique = true)]
public class BaseEntityWithGuid : BaseEntity
{
    protected BaseEntityWithGuid() => Guid = Guid.NewGuid();

    public Guid Guid { get; }
}