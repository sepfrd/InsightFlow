namespace InsightFlow.Common.Dtos;

public class QuestionDto : BaseDto
{
    public UserDto? User { get; set; }

    public string? Title { get; init; }

    public string? Body { get; init; }
}