using SharedKernel.Domain;
using Users.Domain.Entities;

namespace Users.Domain.Events;

record UserCreatedEvent(User User) : DomainEventBase(User.Id)
{
}

