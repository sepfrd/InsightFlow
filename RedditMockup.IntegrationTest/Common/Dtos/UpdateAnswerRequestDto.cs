using RedditMockup.Common.Dtos;
using RedditMockup.IntegrationTest.Common.Enums;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class UpdateAnswerRequestDto : IXunitSerializable
{
    public required int AnswerId { get; set; }

    public required AnswerDto AnswerDto { get; set; }

    public required AnswerControllerTestResult TestResult { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerId = info.GetValue<int>(nameof(AnswerId));
        AnswerDto = info.GetValue<AnswerDto>(nameof(AnswerDto));
        TestResult = info.GetValue<AnswerControllerTestResult>(nameof(TestResult));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerId), AnswerId);
        info.AddValue(nameof(AnswerDto), AnswerDto);
        info.AddValue(nameof(TestResult), TestResult);
    }
}