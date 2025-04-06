using MediatR;
using SharedKernel.Domain;
using Users.Domain.Entities;
using Users.Domain.ValueObjects;

namespace Users.Domain.Events.DomainEvents;

public record UserCreatedEvent(
    Guid AggregateId,
    string UserFirstName,
    string UserLastName,
    Email UserEmail) : DomainEventBase(AggregateId), INotification
{
}

