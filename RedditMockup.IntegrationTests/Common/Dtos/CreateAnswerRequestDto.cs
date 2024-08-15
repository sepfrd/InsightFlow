using System.Net;
using RedditMockup.Common.Dtos;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class CreateAnswerRequestDto
{
    public required AnswerDto AnswerDto { get; init; }

    public required HttpStatusCode ResultStatusCode { get; init; }

    public string? Role { get; init; }
}