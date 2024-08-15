using System.Net;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class DeleteAnswerRequestDto
{
    public required int AnswerId { get; set; }

    public required HttpStatusCode ResultStatusCode { get; set; }

    public string? Role { get; set; }
}