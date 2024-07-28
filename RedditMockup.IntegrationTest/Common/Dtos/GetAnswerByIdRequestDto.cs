using System.Net;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class GetAnswerByIdRequestDto : IXunitSerializable
{
    public required int AnswerId { get; set; }

    public required bool IsAuthenticated { get; set; }

    public required HttpStatusCode ResponseStatusCode { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerId = info.GetValue<int>(nameof(AnswerId));
        IsAuthenticated = info.GetValue<bool>(nameof(IsAuthenticated));
        ResponseStatusCode = info.GetValue<HttpStatusCode>(nameof(ResponseStatusCode));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerId), AnswerId);
        info.AddValue(nameof(IsAuthenticated), IsAuthenticated);
        info.AddValue(nameof(ResponseStatusCode), ResponseStatusCode);
    }
}