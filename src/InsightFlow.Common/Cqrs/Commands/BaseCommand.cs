using InsightFlow.Common.Results;

namespace InsightFlow.Common.Cqrs.Commands;

public abstract class BaseCommand : ICommand<RomaakResult>;

public abstract class BaseCommand<TResult> : ICommand<RomaakResult<TResult>>;