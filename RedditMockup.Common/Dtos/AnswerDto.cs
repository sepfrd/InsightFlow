namespace RedditMockup.Common.Dtos;

public class AnswerDto
{
    // TODO: Remove Id
    public int Id { get; init; }

    public int UserId { get; set; }
    
    public int QuestionId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
}