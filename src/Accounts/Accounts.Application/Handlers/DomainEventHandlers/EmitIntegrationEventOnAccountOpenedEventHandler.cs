using Accounts.Domain.DomainEvents;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.Handlers.DomainEventHandlers;

using Domain.IntegrationEvents;
using Domain.Interfaces;

internal class EmitIntegrationEventOnAccountOpenedEventHandler(
    ILogger<EmitIntegrationEventOnAccountOpenedEventHandler> Logger,
    IEventPublisher<AccountOpenedIntegrationEvent> eventPublisher): INotificationHandler<AccountOpenedEvent>
{
    public async Task Handle(AccountOpenedEvent notification, CancellationToken cancellationToken )
    {

        //I want to emit an integration event in response to a domain event BUT I also want it to be transactional. 
        //In the current implementation I cannot do that because the Aggregate has already been persisted by this pooint.
        var integrationEvent = new AccountOpenedIntegrationEvent($"account opened with id: {notification.AggregateId}");
        await eventPublisher.PublishAsync(integrationEvent);
        
        Logger.LogInformation($"Account opened event emitted for account id: {notification.AggregateId}");
        
    }
}
