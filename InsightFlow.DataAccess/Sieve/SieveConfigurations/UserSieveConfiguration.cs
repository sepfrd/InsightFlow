using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class UserSieveConfiguration : BaseSieveConfiguration<User>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<User>(entity => entity.Username)
            .CanSort()
            .CanFilter();

        mapper
            .Property<User>(entity => entity.Score)
            .CanSort()
            .CanFilter();

        mapper
            .Property<User>(entity => entity.PersonId)
            .CanSort()
            .CanFilter();
    }
}