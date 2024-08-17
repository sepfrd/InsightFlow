using System;
using System.Net;

namespace InsightFlow.IntegrationTests.Common.Dtos;

public class GetAnswerByGuidRequestDto
{
    public required Guid AnswerGuid { get; init; }

    public required HttpStatusCode ResponseStatusCode { get; init; }

    public string? Role { get; set; }
}