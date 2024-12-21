namespace Accounts.Domain.DomainEvents;

using MediatR;
using SharedKernel.Domain.Interfaces;

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
