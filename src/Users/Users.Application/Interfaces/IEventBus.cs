using SharedKernel.Domain.Interfaces;
using SharedKernel.IntegrationEvents;

namespace Users.Application.Interfaces;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken) where T : IIntegrationEvent;
}