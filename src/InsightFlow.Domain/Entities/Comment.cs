using InsightFlow.Domain.Common;

namespace InsightFlow.Domain.Entities;

public class Comment : DomainEntity
{
    public required string Body { get; set; }
    public BlogPost? BlogPost { get; set; }
    public long BlogPostId { get; set; }
    public User? Commenter { get; set; }
    public long CommenterId { get; set; }
}