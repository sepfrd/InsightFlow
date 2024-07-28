using RedditMockup.Common.Dtos;
using RedditMockup.IntegrationTest.Common.Enums;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class CreateAnswerRequestDto : IXunitSerializable
{
    public required AnswerDto AnswerDto { get; set; }

    public required AnswerControllerTestResult TestResult { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerDto = info.GetValue<AnswerDto>(nameof(AnswerDto));
        TestResult = info.GetValue<AnswerControllerTestResult>(nameof(TestResult));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerDto), AnswerDto);
        info.AddValue(nameof(TestResult), TestResult);
    }
}