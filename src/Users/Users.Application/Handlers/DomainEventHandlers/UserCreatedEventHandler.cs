using MediatR;
using Microsoft.Extensions.Logging;
using Users.Domain.Events;
using Users.Domain.Events.DomainEvents;

namespace Users.Application.Handlers.DomainEventHandlers;

public class UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger) : INotificationHandler<UserCreatedEvent>
{
    public Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handled UserCreatedEvent");
        
        //emit a kafka event
        throw new NotImplementedException();
    }
}