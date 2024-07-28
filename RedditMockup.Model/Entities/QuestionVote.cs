using RedditMockup.Model.BaseEntities;
using Sieve.Attributes;

namespace RedditMockup.Model.Entities;

public class QuestionVote : BaseEntity
{
    [Sieve(CanSort = true)]
    public bool Kind { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; set; }
}