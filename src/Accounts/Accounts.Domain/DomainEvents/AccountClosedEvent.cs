namespace Accounts.Domain.DomainEvents;

using SharedKernel.Domain;
using SharedKernel.Domain.Interfaces;

public record AccountClosedEvent(Guid AccountId, Guid AggregateId) : DomainEventBase(AggregateId), IDomainEvent
{

}
