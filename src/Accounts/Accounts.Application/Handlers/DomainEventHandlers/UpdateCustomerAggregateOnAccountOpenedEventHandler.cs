using MediatR;
using Accounts.Domain.DomainEvents;
using Microsoft.Extensions.Logging;

namespace Accounts.Application.Handlers.DomainEventHandlers;
public class UpdateCustomerAggregateOnAccountOpenedEventHandler(ILogger<UpdateCustomerAggregateOnAccountOpenedEventHandler> Logger) : INotificationHandler<AccountOpenedEvent>
{
    public Task Handle(AccountOpenedEvent notification, CancellationToken cancellationToken)
    {
        Logger.LogInformation($"Updating customer aggregate due to update on account aggregate: {notification.AggregateId}");
        //update the customer event
        throw new NotImplementedException();
    }
}
