namespace InsightFlow.Common.Cqrs.Notifications;

public interface INotificationHandler<in T> where T : INotification
{
    Task HandleAsync(T notification, CancellationToken cancellationToken = default);
}