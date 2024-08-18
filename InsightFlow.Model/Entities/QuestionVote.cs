using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class QuestionVote : BaseEntity
{
    public bool Kind { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; set; }
}