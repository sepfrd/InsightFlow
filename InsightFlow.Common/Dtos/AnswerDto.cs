namespace InsightFlow.Common.Dtos;

public class AnswerDto : BaseDto
{
    public Guid QuestionGuid { get; init; }

    public Guid UserGuid { get; init; }

    public required string Body { get; init; }
}