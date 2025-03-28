namespace Accounts.Infrastructure.Events;
public interface IEventPublisher
{
    Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken);
}

