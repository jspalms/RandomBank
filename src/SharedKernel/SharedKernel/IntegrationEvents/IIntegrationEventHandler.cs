namespace SharedKernel.IntegrationEvents;

public interface IIntegrationEventHandler<T> where T : IIntegrationEvent
{
    Task HandleAsync(T @event, CancellationToken cancellationToken = default);
}