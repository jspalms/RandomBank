namespace Accounts.Domain.DomainEvents;

using MediatR;
using SharedKernel.Domain;
using SharedKernel.Domain.Interfaces;

public record AccountOpenedEvent : DomainEventBase, IDomainEvent, INotification
{

}
