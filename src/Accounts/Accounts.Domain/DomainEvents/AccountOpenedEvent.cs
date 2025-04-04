namespace Accounts.Domain.DomainEvents;

using MediatR;
using SharedKernel.Domain;

public record AccountOpenedEvent(Guid AccountId, Guid AggregateId) : DomainEventBase(AggregateId), INotification;

