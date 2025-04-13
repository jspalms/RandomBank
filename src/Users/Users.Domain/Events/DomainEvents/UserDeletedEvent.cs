using SharedKernel.Domain;

namespace Users.Domain.Events.DomainEvents;

public record UserDeletedEvent(Guid AggregateId) : DomainEventBase(AggregateId);