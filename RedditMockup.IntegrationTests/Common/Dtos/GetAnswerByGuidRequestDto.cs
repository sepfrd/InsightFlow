using System;
using System.Net;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class GetAnswerByGuidRequestDto : IXunitSerializable
{
    public required Guid AnswerGuid { get; set; }

    public required HttpStatusCode ResponseStatusCode { get; set; }

    public string? Role { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerGuid = info.GetValue<Guid>(nameof(AnswerGuid));
        ResponseStatusCode = info.GetValue<HttpStatusCode>(nameof(ResponseStatusCode));
        Role = info.GetValue<string?>(nameof(Role));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerGuid), AnswerGuid);
        info.AddValue(nameof(ResponseStatusCode), ResponseStatusCode);
        info.AddValue(nameof(Role), Role);
    }
}