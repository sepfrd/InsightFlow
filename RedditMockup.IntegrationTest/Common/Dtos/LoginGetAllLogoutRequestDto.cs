using RedditMockup.Common.Dtos;
using RedditMockup.IntegrationTest.Common.Enums;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class LoginGetAllLogoutRequestDto : IXunitSerializable
{
    public required LoginDto LoginDto { get; set; }

    public required AuthControllerTestResult TestResult { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        LoginDto = info.GetValue<LoginDto>(nameof(LoginDto));
        TestResult = info.GetValue<AuthControllerTestResult>(nameof(TestResult));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(LoginDto), LoginDto);
        info.AddValue(nameof(TestResult), TestResult);
    }
}