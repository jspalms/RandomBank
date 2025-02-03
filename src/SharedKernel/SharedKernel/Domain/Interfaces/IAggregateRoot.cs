namespace SharedKernel.Domain.Interfaces;

public interface IAggregateRoot
{
    public Guid Id { get;  }
    public IReadOnlyList<IDomainEvent> DomainEvents { get; }
    public void IncrementVersion();
    void ClearDomainEvents();


}