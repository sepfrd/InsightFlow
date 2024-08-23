namespace InsightFlow.Common.Constants;

public class RegexPatternConstants
{
    public const string UsernamePattern = @"^(?![\d_])[\w\d_]{8,32}$";
    public const string PasswordPattern = @"^(?=.*[A-Z])(?=.*\d)(?=.*[.,!@#$%^&*()_+])[A-Za-z\d.,!@#$%^&*()_+]{8,32}$";
}