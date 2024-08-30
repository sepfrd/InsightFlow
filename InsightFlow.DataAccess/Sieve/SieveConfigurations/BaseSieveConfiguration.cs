using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public abstract class BaseSieveConfiguration<T> : ISieveConfiguration
    where T : BaseEntity
{
    public virtual void Configure(SievePropertyMapper mapper)
    {
        mapper
            .Property<T>(entity => entity.Id)
            .CanSort()
            .CanFilter();

        mapper
            .Property<T>(entity => entity.State)
            .CanSort()
            .CanFilter();

        mapper
            .Property<T>(entity => entity.CreationDate)
            .CanSort()
            .CanFilter();

        mapper
            .Property<T>(entity => entity.LastUpdated)
            .CanSort()
            .CanFilter();
    }
}