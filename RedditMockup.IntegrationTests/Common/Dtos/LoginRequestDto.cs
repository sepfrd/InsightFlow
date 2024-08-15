using RedditMockup.Common.Dtos;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class LoginRequestDto : IXunitSerializable
{
    public required LoginDto LoginDto { get; set; }

    public required bool ShouldSucceed { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        LoginDto = info.GetValue<LoginDto>(nameof(LoginDto));
        ShouldSucceed = info.GetValue<bool>(nameof(ShouldSucceed));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(LoginDto), LoginDto);
        info.AddValue(nameof(ShouldSucceed), ShouldSucceed);
    }
}