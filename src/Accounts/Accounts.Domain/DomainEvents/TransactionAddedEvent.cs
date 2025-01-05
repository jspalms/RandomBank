namespace Accounts.Domain.DomainEvents;

using MediatR;
using SharedKernel.Domain;
using SharedKernel.Domain.Interfaces;

public record TransactionAddedEvent : DomainEventBase, IDomainEvent, INotification
{
    // should add more infomration to these events at some point
}