using Accounts.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.Handlers.DomainEventHandlers;
internal class EmitIntegrationEventOnAccountOpenedEventHandler(ILogger<EmitIntegrationEventOnAccountOpenedEventHandler> Logger): INotificationHandler<AccountOpenedEvent>
{
    public Task Handle(AccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Account opened event emitted for account id: {notification.AggregateId}");

        //I want to emit an integration event in response to a domain event BUT I also want it to be transactional. 
        //In the current implementation I cannot do that because the Aggregate has already been persisted by this pooint.

        throw new NotImplementedException();
    }

    //Emit integration events

    
}
