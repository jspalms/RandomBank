using MediatR;

namespace Accounts.Domain.DomainEvents;
public record AccountOpenedEvent : IDomainEvent, INotification
{
    public Guid EventId { get; init; } = Guid.NewGuid();
    public DateTime CreatedOn { get; init; } = DateTime.UtcNow;
    public Guid AggregateId { get; init; }
    public AccountOpenedEvent(Guid aggregateId)
    {
        AggregateId = aggregateId;
    }
}
