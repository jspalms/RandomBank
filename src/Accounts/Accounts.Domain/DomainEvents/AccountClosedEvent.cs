namespace Accounts.Domain.DomainEvents;
public record AccountClosedEvent : IDomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public Guid AggregateId { get; init; }
    public AccountClosedEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}
