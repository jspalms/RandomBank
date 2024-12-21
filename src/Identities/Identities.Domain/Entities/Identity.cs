namespace Identities.Domain.Entities;

using SharedKernel.Domain.Interfaces;

public class Identity: IAggregateRoot
{
    public Guid Id { get; }
}