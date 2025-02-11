using InsightFlow.Domain.Common;
using InsightFlow.Domain.Enums;

namespace InsightFlow.Domain.Entities;

public class Reaction : DomainEntity
{
    public IReactable? Content { get; set; }
    public long ContentId { get; set; }
    public User? Reactor { get; set; }
    public long ReactorId { get; set; }
    public ReactionType Type { get; set; }
}