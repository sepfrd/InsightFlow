namespace InsightFlow.Domain.Common;

public sealed record Error(int Code, string? Description = null)
{
    public static readonly Error None = new(-1);
}