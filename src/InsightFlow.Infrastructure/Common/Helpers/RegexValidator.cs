using System.Text.RegularExpressions;
using InsightFlow.Domain.Common;

namespace InsightFlow.Infrastructure.Common.Helpers;

public partial class RegexValidator
{
    [GeneratedRegex(DomainConstants.UsernameRegexPattern)]
    public static partial Regex UsernameRegex();

    [GeneratedRegex(DomainConstants.PasswordRegexPattern)]
    public static partial Regex PasswordRegex();
}