namespace Accounts.Domain.DomainEvents;
internal record AccountOpenedEvent : IDomainEvent
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public Guid AggregateId { get; init; }
    public AccountOpenedEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}
