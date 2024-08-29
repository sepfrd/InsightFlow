namespace InsightFlow.Model.Entities;

public class Answer : BaseEntity
{
    public required string Body { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; init; }

    public int UserId { get; set; }

    public User? User { get; init; }
}