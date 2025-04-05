using SharedKernel.Domain.Interfaces;

namespace Users.Application.Interfaces;

public interface IEventBus
{
    Task PublishWithoutKeyAsync<T>(T @event, CancellationToken cancellationToken) where T : IIntegrationEvent;
}