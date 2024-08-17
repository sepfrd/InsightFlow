using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class Question : BaseEntityWithGuid
{
    [Sieve(CanFilter = true, CanSort = true)]
    public string? Title { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public string? Description { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<QuestionVote>? Votes { get; set; }

    public ICollection<Bookmark>? Bookmarks { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int UserId { get; set; }

    public User? User { get; set; }
}