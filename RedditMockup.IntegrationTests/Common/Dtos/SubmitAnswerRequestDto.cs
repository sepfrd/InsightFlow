using System.Net;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTests.Common.Dtos;

public class SubmitAnswerRequestDto : IXunitSerializable
{
    public required int AnswerId { get; set; }

    public required bool VoteKind { get; set; }

    public required HttpStatusCode ResultStatusCode { get; set; }

    public string? Role { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerId = info.GetValue<int>(nameof(AnswerId));
        VoteKind = info.GetValue<bool>(nameof(VoteKind));
        ResultStatusCode = info.GetValue<HttpStatusCode>(nameof(ResultStatusCode));
        Role = info.GetValue<string?>(nameof(Role));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerId), AnswerId);
        info.AddValue(nameof(VoteKind), VoteKind);
        info.AddValue(nameof(ResultStatusCode), ResultStatusCode);
        info.AddValue(nameof(Role), Role);
    }
}