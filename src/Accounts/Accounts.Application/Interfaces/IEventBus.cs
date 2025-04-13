using SharedKernel.IntegrationEvents;

namespace Accounts.Application.Interfaces;

public interface IEventBus
{
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken) where T : IIntegrationEvent;
    Task ConsumeTopicsAsync(List<string> topics, CancellationToken cancellationToken);

}