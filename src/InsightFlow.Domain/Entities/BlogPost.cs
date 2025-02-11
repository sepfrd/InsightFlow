using InsightFlow.Domain.Common;

namespace InsightFlow.Domain.Entities;

public class BlogPost : DomainEntity, IReactable
{
    public required string Title { get; set; }
    public required string Body { get; set; }
    public User? Author { get; set; }
    public long AuthorId { get; set; }
}