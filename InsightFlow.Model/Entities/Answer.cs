using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class Answer : BaseEntityWithGuid
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string? Title { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string? Description { get; set; }

    public ICollection<AnswerVote>? Votes { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int QuestionId { get; set; }

    public Question? Question { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int UserId { get; set; }

    public User? User { get; set; }
}