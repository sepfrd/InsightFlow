using InsightFlow.Model.BaseEntities;
using Sieve.Attributes;

namespace InsightFlow.Model.Entities;

public class AnswerVote : BaseEntity
{
    [Sieve(CanFilter = true, CanSort = true)]
    public bool Kind { get; set; }

    [Sieve(CanFilter = true, CanSort = true)]
    public int AnswerId { get; set; }

    public Answer? Answer { get; set; }
}