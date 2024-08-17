using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class QuestionVote : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public bool Kind { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int QuestionId { get; set; }

    public Question? Question { get; set; }
}