using InsightFlow.Infrastructure.Common.Helpers;

namespace InsightFlow.Application.UnitTests.Features.TestUtilities.Constants;

public static partial class Constants
{
    public static class User
    {
        public const long Id = 1L;
        public const string Username = "sepehr_frd";
        public const string Email = "sepfrd@outlook.com";
        public const string FirstName = "Sepehr";
        public const string LastName = "Foroughi Rad";
        public static readonly string PasswordHash = PasswordHelper.HashPassword("Sfr1376.");
        public static readonly Guid Uuid = Guid.Parse("a3513538-87f9-4be0-9780-37438e6f8c1e");

        public static Domain.Entities.User CreateUser(
            Guid? uuid = null,
            string? username = null,
            string? passwordHash = null,
            string? email = null,
            string? firstName = null,
            string? lastName = null,
            Domain.Entities.UserRole[]? userRoles = null,
            Domain.Entities.BlogPost[]? blogPosts = null) =>
            new()
            {
                Id = Id,
                Uuid = uuid ?? Uuid,
                Username = username ?? Username,
                PasswordHash = passwordHash ?? PasswordHash,
                Email = email ?? Email,
                FirstName = firstName ?? FirstName,
                LastName = lastName ?? LastName,
                UserRoles = userRoles ?? [UserRole.CreateUserRole()],
                BlogPosts = blogPosts ?? []
            };
    }
}