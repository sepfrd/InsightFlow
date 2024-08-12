using System.Net;
using RedditMockup.Common.Dtos;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class CreateAnswerRequestDto : IXunitSerializable
{
    public required AnswerDto AnswerDto { get; set; }

    public required HttpStatusCode ResultStatusCode { get; set; }

    public string? Role { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerDto = info.GetValue<AnswerDto>(nameof(AnswerDto));
        ResultStatusCode = info.GetValue<HttpStatusCode>(nameof(ResultStatusCode));
        Role = info.GetValue<string?>(nameof(Role));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerDto), AnswerDto);
        info.AddValue(nameof(ResultStatusCode), ResultStatusCode);
        info.AddValue(nameof(Role), Role);
    }
}