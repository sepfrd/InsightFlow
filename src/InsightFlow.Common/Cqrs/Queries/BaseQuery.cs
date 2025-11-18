using InsightFlow.Common.Results;

namespace InsightFlow.Common.Cqrs.Queries;

public abstract class BaseQuery<TResult> : IQuery<RomaakResult<TResult>>;