namespace SharedKernel.IntegrationEvents;

public interface IIntegrationEvent
{
    Guid EventId { get; }
    DateTime CreatedOn { get; }
}

