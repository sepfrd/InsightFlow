namespace RedditMockup.Common.Dtos;

public class AnswerDto : BaseDto
{
    public int QuestionId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}