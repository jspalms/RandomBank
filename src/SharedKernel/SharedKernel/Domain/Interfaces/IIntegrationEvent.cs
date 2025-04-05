namespace SharedKernel.Domain.Interfaces;

public interface IIntegrationEvent
{
    Guid EventId { get; set; }
    DateTime CreatedOn { get; }
}

