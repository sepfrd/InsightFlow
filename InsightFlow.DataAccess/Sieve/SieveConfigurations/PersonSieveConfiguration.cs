using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class PersonSieveConfiguration : BaseSieveConfiguration<Person>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Person>(entity => entity.FirstName)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Person>(entity => entity.LastName)
            .CanSort()
            .CanFilter();
    }
}