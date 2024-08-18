using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class Answer : BaseEntityWithGuid
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public ICollection<AnswerVote>? Votes { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }
}