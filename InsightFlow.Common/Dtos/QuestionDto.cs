namespace InsightFlow.Common.Dtos;

public class QuestionDto : BaseDto
{
    public Guid UserGuid { get; init; }

    public required string Title { get; init; }

    public required string Body { get; init; }
}