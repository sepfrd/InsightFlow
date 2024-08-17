using InsightFlow.Common.Dtos;
using InsightFlow.IntegrationTests.Common.Enums;

namespace InsightFlow.IntegrationTests.Common.Dtos;

public class LoginGetAllLogoutRequestDto
{
    public required LoginDto LoginDto { get; init; }

    public required AuthControllerTestResult TestResult { get; init; }
}