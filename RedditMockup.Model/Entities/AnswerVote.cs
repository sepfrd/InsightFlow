using RedditMockup.Model.BaseEntities;
using Sieve.Attributes;

namespace RedditMockup.Model.Entities;

public class AnswerVote : BaseEntity
{
    [Sieve(CanSort = true)]
    public bool Kind { get; set; }

    public int AnswerId { get; set; }

    public Answer? Answer { get; set; }
}