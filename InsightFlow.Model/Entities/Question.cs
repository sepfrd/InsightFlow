using InsightFlow.Model.BaseEntities;

namespace InsightFlow.Model.Entities;

public class Question : BaseEntityWithGuid
{
    public string? Title { get; set; }

    public string? Description { get; set; }

    public ICollection<Answer>? Answers { get; set; }

    public ICollection<QuestionVote>? Votes { get; set; }

    public ICollection<Bookmark>? Bookmarks { get; set; }

    public int UserId { get; set; }

    public User? User { get; set; }
}