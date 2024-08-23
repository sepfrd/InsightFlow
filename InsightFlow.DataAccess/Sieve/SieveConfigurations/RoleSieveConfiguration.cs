using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class RoleSieveConfiguration : BaseSieveConfiguration<Role>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Role>(entity => entity.Name)
            .CanSort()
            .CanFilter();
    }
}