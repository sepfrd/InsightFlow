using RedditMockup.IntegrationTest.Common.Enums;
using Xunit.Abstractions;

namespace RedditMockup.IntegrationTest.Common.Dtos;

public class DeleteAnswerRequestDto : IXunitSerializable
{
    public required int AnswerId { get; set; }

    public required AnswerControllerTestResult TestResult { get; set; }

    public void Deserialize(IXunitSerializationInfo info)
    {
        AnswerId = info.GetValue<int>(nameof(AnswerId));
        TestResult = info.GetValue<AnswerControllerTestResult>(nameof(TestResult));
    }

    public void Serialize(IXunitSerializationInfo info)
    {
        info.AddValue(nameof(AnswerId), AnswerId);
        info.AddValue(nameof(TestResult), TestResult);
    }
}