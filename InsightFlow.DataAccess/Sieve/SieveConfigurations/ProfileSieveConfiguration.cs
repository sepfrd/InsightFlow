using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class ProfileSieveConfiguration : BaseSieveConfiguration<Profile>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<Profile>(entity => entity.Bio)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Profile>(entity => entity.Email)
            .CanSort()
            .CanFilter();

        mapper
            .Property<Profile>(entity => entity.UserId)
            .CanSort()
            .CanFilter();
    }
}