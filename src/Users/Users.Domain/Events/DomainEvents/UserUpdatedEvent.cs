using SharedKernel.Domain;
using Users.Domain.ValueObjects;

namespace Users.Domain.Events.DomainEvents;

public record UserUpdatedEvent(
    Guid AggregateId,
    string UserFirstName,
    string UserLastName,
    Email UserEmail)  : DomainEventBase(AggregateId);