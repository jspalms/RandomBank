using Microsoft.Extensions.Hosting;
using Users.Application.Interfaces;

namespace Users.Infrastructure.Events.IntegrationEvents;

public class IntegrationEventConsumer(IEventBus eventBus) : BackgroundService
{ 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        //Need to yield to not block the main thread might be a better way
        await Task.Yield();
        await eventBus.ConsumeTopicsAsync(stoppingToken);
    }
}