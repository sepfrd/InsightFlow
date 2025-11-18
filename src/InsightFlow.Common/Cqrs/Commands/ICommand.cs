namespace InsightFlow.Common.Cqrs.Commands;

public interface ICommand;

public interface ICommand<TResponse> : ICommand;