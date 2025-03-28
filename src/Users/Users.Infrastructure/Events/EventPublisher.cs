using System.Text.Json;
using Accounts.Infrastructure.Events;
using MediatR;
using Microsoft.Extensions.Logging;
using Users.Infrastructure.Exceptions;

namespace Users.Infrastructure.Events;

public class EventPublisher : IEventPublisher
{
    private readonly IMediator _mediator;
    private readonly ILogger<EventPublisher> _logger;

    public EventPublisher(IMediator mediator, ILogger<EventPublisher> logger)
    {
        _mediator = mediator;
        _logger = logger;
    }

    public async Task PublishAsync(string eventType, string payload, CancellationToken cancellationToken)
    {
        try
        {
            //Gets the type which the event should be 
            var eventTypeInstance = Type.GetType(eventType);
            if (eventTypeInstance == null)
            {
                _logger.LogError("Unknown event type: {EventType}", eventType);
                throw new EventTypeException($"Unknown event type: {eventType}");
            }
            //Deserializes the event payload into the type specified
            var domainEvent = JsonSerializer.Deserialize(payload, eventTypeInstance);
            if (domainEvent == null)
            {
                _logger.LogError("Failed to deserialize event: {Payload}", payload);
                return;
            }

            await _mediator.Publish(domainEvent, cancellationToken);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error publishing event");
            throw;
        }
    }
}