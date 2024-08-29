namespace InsightFlow.Common.Dtos;

public class QuestionDto : BaseDto
{
    public UserDto? AskingUser { get; set; }

    public string? Title { get; init; }

    public string? Body { get; init; }
}