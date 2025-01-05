namespace SharedKernel.Domain.Interfaces;

public interface IAggregateRoot
{
    public Guid Id { get;  }
    public void IncrementVersion();
}