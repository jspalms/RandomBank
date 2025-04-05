using SharedKernel.Domain.Interfaces;

namespace SharedKernel.Domain;

public abstract class IntegrationEventBase : IIntegrationEvent
{
    public Guid EventId { get; set; } = Guid.NewGuid();
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
}

