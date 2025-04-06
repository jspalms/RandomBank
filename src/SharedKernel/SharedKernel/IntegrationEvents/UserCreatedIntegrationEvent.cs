using SharedKernel.Domain;

namespace SharedKernel.IntegrationEvents;

public record UserCreatedIntegrationEvent(
    Guid UserAggregateId, 
    string UserFirstName, 
    string UserLastName,
    string UserEmail,
    Guid? eventId = null) : IIntegrationEvent
{
    public Guid EventId { get; } = eventId ?? Guid.NewGuid();
    public DateTime CreatedOn { get; } = DateTime.Now;
    public string EventType => nameof(UserCreatedIntegrationEvent);
}