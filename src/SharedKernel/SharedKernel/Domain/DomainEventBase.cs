namespace SharedKernel.Domain;

using Interfaces;

public abstract record DomainEventBase(Guid AggregateId) : IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime CreatedOn { get; } = DateTime.UtcNow;
    public Guid AggregateId { get; init; } = AggregateId;
}