using Microsoft.Extensions.Hosting;
using Users.Application.Interfaces;

namespace Users.Infrastructure.Events.IntegrationEvents;

public class IntegrationEventConsumer(IEventBus eventBus) : BackgroundService
{ 
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        await eventBus.ConsumeTopicsAsync(stoppingToken);
    }
}