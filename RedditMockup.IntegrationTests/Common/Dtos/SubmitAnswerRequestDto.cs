using System.Net;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class SubmitAnswerRequestDto
{
    public required int AnswerId { get; set; }

    public required bool VoteKind { get; set; }

    public required HttpStatusCode ResultStatusCode { get; set; }

    public string? Role { get; set; }
}