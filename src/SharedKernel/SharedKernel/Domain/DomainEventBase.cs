namespace SharedKernel.Domain;

public abstract record DomainEventBase
{
    public Guid EventId { get; } = Guid.NewGuid();
    public DateTime CreatedOn { get; } = DateTime.UtcNow;   
    public required Guid AggregateId { get; init; }     
}