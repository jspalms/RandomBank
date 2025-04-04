using MediatR;
using SharedKernel.Domain;
using Users.Domain.Entities;

namespace Users.Domain.Events.DomainEvents;

public record UserCreatedEvent(Guid Id) : DomainEventBase(Id), INotification
{
}

