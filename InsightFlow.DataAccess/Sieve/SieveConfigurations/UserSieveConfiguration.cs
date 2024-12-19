using InsightFlow.Model.Entities;
using Sieve.Services;

namespace InsightFlow.DataAccess.Sieve.SieveConfigurations;

public class UserSieveConfiguration : BaseSieveConfiguration<User>
{
    public override void Configure(SievePropertyMapper mapper)
    {
        base.Configure(mapper);

        mapper
            .Property<User>(user => user.Username)
            .CanSort()
            .CanFilter();

        mapper
            .Property<User>(user => user.Email)
            .CanSort()
            .CanFilter();

        mapper
            .Property<User>(user => user.FirstName)
            .CanSort()
            .CanFilter();

        mapper
            .Property<User>(user => user.LastName)
            .CanSort()
            .CanFilter();

        mapper
            .Property<User>(user => user.UserRoles.Select(userRole => userRole.RoleId).First())
            .CanSort()
            .CanFilter();
    }
}