using System.Net;
using RedditMockup.Common.Dtos;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class UpdateAnswerRequestDto : IXunitSerializable
{
    public required int AnswerId { get; set; }

    public required AnswerDto AnswerDto { get; set; }

    public required HttpStatusCode ResultStatusCode { get; set; }

    public string? Role { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerId = info.GetValue<int>(nameof(AnswerId));
        AnswerDto = info.GetValue<AnswerDto>(nameof(AnswerDto));
        ResultStatusCode = info.GetValue<HttpStatusCode>(nameof(ResultStatusCode));
        Role = info.GetValue<string?>(nameof(Role));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerId), AnswerId);
        info.AddValue(nameof(AnswerDto), AnswerDto);
        info.AddValue(nameof(ResultStatusCode), ResultStatusCode);
        info.AddValue(nameof(Role), Role);
    }
}