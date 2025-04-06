using SharedKernel.IntegrationEvents;

namespace SharedKernel.Domain.Interfaces;

public interface IIntegrationEventWithKey : IIntegrationEvent
{
    string Key { get; }
}
