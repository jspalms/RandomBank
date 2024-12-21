namespace SharedKernel.Domain.Interfaces;

public interface IDomainEvent
{
    Guid EventId { get; }         
    DateTime CreatedOn { get; }   
    Guid AggregateId { get; }     
}