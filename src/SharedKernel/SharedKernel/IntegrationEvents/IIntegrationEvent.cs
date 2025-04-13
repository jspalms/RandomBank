namespace SharedKernel.IntegrationEvents;

public interface IIntegrationEvent
{
    Guid EventId { get; }
    DateTimeOffset CreatedOn { get; }
    string EventTypeName { get; }
    string Payload { get; }
}

