using MediatR;
using SharedKernel.Domain;
using Users.Domain.Entities;

namespace Users.Domain.Events.DomainEvents;

public record UserCreatedEvent(User User) : DomainEventBase(User.Id), INotification
{
}

