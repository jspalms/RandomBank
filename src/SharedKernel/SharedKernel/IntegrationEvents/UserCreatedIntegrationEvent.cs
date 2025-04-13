using System.Text.Json;
using SharedKernel.Domain;

namespace SharedKernel.IntegrationEvents;

public record UserCreatedIntegrationEvent(
    Guid UserAggregateId, 
    string UserFirstName, 
    string UserLastName,
    string UserEmail,
    Guid? eventId = null) : IIntegrationEvent
{
    public string EventType => nameof(UserCreatedIntegrationEvent);
    public Guid EventId => eventId ?? Guid.NewGuid();
    public DateTimeOffset CreatedOn => DateTimeOffset.UtcNow;
    public string Payload => JsonSerializer.Serialize(this);
    public string EventTypeName => GetType().FullName ?? throw new InvalidOperationException("Event type name cannot be null");
}