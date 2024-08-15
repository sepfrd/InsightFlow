using System.Net;
using RedditMockup.Common.Dtos;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class UpdateAnswerRequestDto
{
    public required int AnswerId { get; set; }

    public required AnswerDto AnswerDto { get; set; }

    public required HttpStatusCode ResultStatusCode { get; set; }

    public string? Role { get; set; }
}