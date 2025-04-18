﻿using MediatR;
using Microsoft.Extensions.Logging;
using SharedKernel.IntegrationEvents;
using Users.Application.Interfaces;
using Users.Domain.Events.DomainEvents;

namespace Users.Application.Handlers.DomainEventHandlers;

public class UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger,
IEventBus eventBus) : INotificationHandler<UserCreatedEvent>
{
    public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handled UserCreatedEvent");
        
        var @event = new UserCreatedIntegrationEvent(
            notification.AggregateId, 
            notification.UserFirstName,
            notification.UserLastName,
            notification.UserEmail.Value);
        
        await eventBus.PublishAsync(@event, cancellationToken);
    }
}