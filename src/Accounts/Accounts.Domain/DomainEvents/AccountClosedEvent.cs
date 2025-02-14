namespace Accounts.Domain.DomainEvents;

using MediatR;
using SharedKernel.Domain;

public record AccountClosedEvent(Guid AccountId, Guid AggregateId) : DomainEventBase(AggregateId), INotification
{

}
