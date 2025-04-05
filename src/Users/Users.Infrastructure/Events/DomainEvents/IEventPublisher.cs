namespace Users.Infrastructure.Events.DomainEvents;
public interface IEventPublisher
{
    Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken);
}

