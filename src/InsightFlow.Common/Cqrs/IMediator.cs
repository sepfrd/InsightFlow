using InsightFlow.Common.Cqrs.Commands;
using InsightFlow.Common.Cqrs.Notifications;
using InsightFlow.Common.Cqrs.Queries;

namespace InsightFlow.Common.Cqrs;

public interface IMediator
{
    Task<TResult> SendAsync<TResult>(ICommand<TResult> command, CancellationToken cancellationToken = default);

    Task<TResult> SendAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);

    Task PublishAsync<TNotification>(TNotification notification, CancellationToken cancellationToken = default)
        where TNotification : INotification;
}