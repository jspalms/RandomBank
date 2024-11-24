using Accounts.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.Handlers.DomainEventHandlers;
internal class EmitIntegrationEventOnAccountOpenedEventHandler(ILogger<EmitIntegrationEventOnAccountOpenedEventHandler> Logger): INotificationHandler<AccountOpenedEvent>
{
    public Task Handle(AccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Account opened event emitted for account id: {notification.AggregateId}");
        throw new NotImplementedException();
    }

    //Emit integration events

    
}
