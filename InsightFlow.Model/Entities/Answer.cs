namespace InsightFlow.Model.Entities;

public class Answer : BaseEntity
{
    public required string Title { get; set; }

    public required string Description { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }
}