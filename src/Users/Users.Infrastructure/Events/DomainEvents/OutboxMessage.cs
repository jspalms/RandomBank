using System.Text.Json;
using SharedKernel.Domain.Interfaces;
using Users.Infrastructure.Exceptions;

namespace Users.Infrastructure.Events.DomainEvents;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = default!;
    public string Payload { get; set; } = default!;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }
    public int ErrorCount { get; set; }

    public OutboxMessage()
    {

    }
    
    public OutboxMessage(IDomainEvent domainEvent)
    {
        Id = domainEvent.EventId;
        Type = domainEvent.GetType().AssemblyQualifiedName ?? throw new EventTypeException("No assembly qualified name found for domain event type");
        //Must cast to an object to preserved properties from derived classes
        Payload = JsonSerializer.Serialize((object)domainEvent);
        OccurredOn = domainEvent.CreatedOn;
    }
}
