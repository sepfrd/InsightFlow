using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class AnswerVote : BaseEntity
{
    public bool Kind { get; set; }

    public int AnswerId { get; set; }

    public Answer? Answer { get; set; }
}