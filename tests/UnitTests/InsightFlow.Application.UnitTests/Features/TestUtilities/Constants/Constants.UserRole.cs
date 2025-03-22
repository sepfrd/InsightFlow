namespace InsightFlow.Application.UnitTests.Features.TestUtilities.Constants;

public static partial class Constants
{
    public static class UserRole
    {
        public const long Id = 1L;

        public static Domain.Entities.UserRole CreateUserRole(Domain.Entities.User? user = null, Domain.Entities.Role? role = null) =>
            new()
            {
                Id = Id,
                UserId = user?.Id ?? User.Id,
                RoleId = role?.Id ?? Role.Id
            };
    }
}