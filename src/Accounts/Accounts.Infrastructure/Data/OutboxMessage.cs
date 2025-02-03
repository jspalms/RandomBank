namespace Accounts.Infrastructure.Data;

using SharedKernel.Domain.Interfaces;
using System.Text.Json;

public class OutboxMessage
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Type { get; set; } = default!;
    public string Payload { get; set; } = default!;
    public DateTime OccurredOn { get; set; } = DateTime.UtcNow;
    public DateTime? ProcessedOn { get; set; }
    public string? Error { get; set; }

    public OutboxMessage(IDomainEvent domainEvent)
    {
        Id = domainEvent.EventId;
        Type = domainEvent.GetType().Name;
        Payload = JsonSerializer.Serialize(domainEvent);
        OccurredOn = domainEvent.CreatedOn;
    }
}
