using RedditMockup.Common.Dtos;
using RedditMockup.IntegrationTests.Common.Enums;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class LoginGetAllLogoutRequestDto
{
    public required LoginDto LoginDto { get; init; }

    public required AuthControllerTestResult TestResult { get; init; }
}