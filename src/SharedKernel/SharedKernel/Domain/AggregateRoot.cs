namespace SharedKernel.Domain;

using Interfaces;

public abstract class AggregateRoot: IAggregateRoot
{
    public Guid Id { get; protected set; }
    protected int Version { get; set; }
    protected List<IDomainEvent> _domainEvents { get; } = new List<IDomainEvent>();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }

}
