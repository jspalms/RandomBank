using Microsoft.Extensions.Hosting;
using Users.Application.Interfaces;

namespace Users.Infrastructure.Events.IntegrationEvents;

public class BackgroundConsumer(IEventBus eventBus) : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        throw new NotImplementedException();
    }
}