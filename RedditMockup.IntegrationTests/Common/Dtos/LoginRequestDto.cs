using RedditMockup.Common.Dtos;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class LoginRequestDto
{
    public required LoginDto LoginDto { get; init; }

    public required bool ShouldSucceed { get; init; }
}