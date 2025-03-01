namespace InsightFlow.Infrastructure.Common.Constants;

public static class ApplicationConstants
{
    // ---------------------------- Auth Constants ----------------------------
    public const string AdminPolicyName = "OnlyAdminPolicy";
    public const string UserPolicyName = "OnlyUserPolicy";
    public const string AdminRoleName = "Admin";
    public const string UserRoleName = "User";
    public const string UsernameClaim = "username";
    public const string ExternalIdClaim = "external_id";

    // ---------------------------- CORS Constants ----------------------------
    public const string AllowAnyOriginCorsPolicy = "AllowAnyOriginCorsPolicy";
    public const string RestrictedCorsPolicy = "RestrictedCorsPolicy";

    // ---------------------------- Configuration Key Constants ----------------------------
    public const string ApplicationVersionConfigurationKey = "ApplicationVersion";
    public const string ApplicationUrlsConfigurationSectionKey = "ApplicationUrls";
    public const string SqlServerConfigurationKey = "SqlServer";
    public const string ServerUrlConfigurationKey = "ServerUrl";
    public const string ClientUrlConfigurationKey = "ClientUrl";
    public const string JwtConfigurationSectionKey = "JwtConfiguration";
    public const string JwtPublicKeyConfigurationKey = "PublicKey";
    public const string JwtPrivateKeyConfigurationKey = "PrivateKey";

    // ---------------------------- Pagination Constants ----------------------------
    public const int MinimumPageSize = 10;
    public const int MaximumPageSize = 100;

    // ---------------------------- Rate Limiting Constants ----------------------------
    public const string FixedWindowRateLimiterPolicy = "FixedWindowRateLimiterPolicy";
    public const string ConcurrencyRateLimiterPolicy = "ConcurrencyRateLimiterPolicy";

    public const string TestingEnvironmentName = "Testing";
    public const string ApplicationName = "InsightFlow";
    public const string ApplicationContactName = "Sepehr Foroughi Rad";
    public const string ApplicationContactEmail = "sepfrd@outlook.com";
    public const string ApplicationContactUrl = "https://github.com/sepehr-frd";
}