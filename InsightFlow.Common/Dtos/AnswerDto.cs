namespace InsightFlow.Common.Dtos;

public class AnswerDto : BaseDto
{
    public UserDto? User { get; set; }

    public Guid QuestionGuid { get; init; }

    public string? Body { get; init; }
}