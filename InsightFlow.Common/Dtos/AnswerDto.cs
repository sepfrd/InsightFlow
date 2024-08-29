namespace InsightFlow.Common.Dtos;

public class AnswerDto : BaseDto
{
    public UserDto? AnsweringUser { get; set; }

    public Guid QuestionGuid { get; init; }

    public string? Body { get; init; }
}