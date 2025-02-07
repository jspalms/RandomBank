namespace Accounts.Application.Handlers.DomainEventHandlers;

using Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

public class AccountCreatedDomainEventHandler(ILogger<AccountCreatedDomainEventHandler> logger) : INotificationHandler<AccountOpenedEvent>
{
    public Task Handle(AccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handled AccountOpenedEvent");
        return Task.CompletedTask;

        //emit a kafka event
    }
}