namespace Accounts.Domain.Base;

public abstract class AggregateRoot
{
    public Guid Id { get; protected set; }
    protected List<IDomainEvent> _domainEvents { get; } = new List<IDomainEvent>();
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    protected void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
}
