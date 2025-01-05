namespace Accounts.Domain.DomainEvents;

using SharedKernel.Domain;
using SharedKernel.Domain.Interfaces;

public record AccountClosedEvent : DomainEventBase, IDomainEvent
{

}
