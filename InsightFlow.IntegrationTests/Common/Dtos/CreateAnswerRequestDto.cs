using System.Net;
using InsightFlow.Common.Dtos;

namespace InsightFlow.IntegrationTests.Common.Dtos;

public class CreateAnswerRequestDto
{
    public required AnswerDto AnswerDto { get; init; }

    public required HttpStatusCode ResultStatusCode { get; init; }

    public string? Role { get; init; }
}