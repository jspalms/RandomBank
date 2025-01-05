namespace SharedKernel.Domain;

using Interfaces;

public abstract class AggregateRootBase: IAggregateRoot
{
    public Guid Id { get; protected set; }
    private int VersionNumber { get; set; }
    private List<IDomainEvent> _domainEvents { get; } = [];
    public IReadOnlyList<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();
    protected void AddDomainEvent(IDomainEvent domainEvent)
    {
        _domainEvents.Add(domainEvent);
    }
    public void ClearDomainEvents()
    {
        _domainEvents.Clear();
    }
    public void IncrementVersion()
    {
        VersionNumber++;
    }
}
