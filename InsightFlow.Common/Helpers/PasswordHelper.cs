using BCrypt.Net;

namespace InsightFlow.Common.Helpers;

public static class PasswordHelper
{
    public static string HashPassword(string password) =>
        BCrypt.Net.BCrypt.EnhancedHashPassword(password, HashType.SHA512, 12);

    public static bool ValidatePassword(string password, string passwordHash) =>
        BCrypt.Net.BCrypt.EnhancedVerify(password, passwordHash, HashType.SHA512);
}