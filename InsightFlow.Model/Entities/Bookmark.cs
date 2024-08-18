using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class Bookmark : BaseEntity
{
    public bool IsBookmarked { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }

    public int QuestionId { get; set; }

    public Question? Question { get; set; }
}