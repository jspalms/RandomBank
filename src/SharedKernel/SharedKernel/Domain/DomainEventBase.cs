namespace SharedKernel.Domain;

using Interfaces;

public abstract record DomainEventBase: IDomainEvent
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime CreatedOn { get; } = DateTime.UtcNow;   
    public required Guid AggregateId { get; init; }     
}