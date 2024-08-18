using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class UserRoleSieveConfiguration : BaseSieveConfiguration<UserRole>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<UserRole>(entity => entity.UserId)
            .CanSort()
            .CanFilter();

        mapper
            .Property<UserRole>(entity => entity.RoleId)
            .CanSort()
            .CanFilter();
    }
}