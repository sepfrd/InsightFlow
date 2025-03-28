namespace InsightFlow.Infrastructure.Common.Constants;

public static class InfrastructureConstants
{
    // ---------------------------- Auth Constants ----------------------------
    public const string AdminPolicyName = "OnlyAdminPolicy";
    public const string UserPolicyName = "OnlyUserPolicy";
    public const string UsernameClaim = "username";
    public const string UuidClaim = "uuid";

    // ---------------------------- CORS Constants ----------------------------
    public const string AllowAnyOriginCorsPolicy = "AllowAnyOriginCorsPolicy";
    public const string RestrictedCorsPolicy = "RestrictedCorsPolicy";

    // ---------------------------- Rate Limiting Constants ----------------------------
    public const string FixedWindowRateLimiterPolicy = "FixedWindowRateLimiterPolicy";
    public const string ConcurrencyRateLimiterPolicy = "ConcurrencyRateLimiterPolicy";

    public const string TestingEnvironmentName = "Testing";
}