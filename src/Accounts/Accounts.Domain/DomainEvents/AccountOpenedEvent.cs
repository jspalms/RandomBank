namespace Accounts.Domain.DomainEvents;

using MediatR;
using SharedKernel.Domain;
using SharedKernel.Domain.Interfaces;

public record AccountOpenedEvent(Guid AccountId, Guid AggregateId) : DomainEventBase(AggregateId), INotification;

