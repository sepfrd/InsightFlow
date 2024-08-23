namespace InsightFlow.Model.Entities;

public class Question : BaseEntity
{
    public required string Title { get; set; }

    public required string Body { get; set; }

    public ICollection<Answer> Answers { get; set; } = [];

    public int UserId { get; set; }

    public User? User { get; set; }
}