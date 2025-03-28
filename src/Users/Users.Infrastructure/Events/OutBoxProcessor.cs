using Accounts.Infrastructure.Events;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Users.Infrastructure.Data;

namespace Users.Infrastructure.Events;

public class OutboxProcessor : BackgroundService
{
    private readonly IServiceScopeFactory _scopeFactory;
    private readonly IEventPublisher _eventPublisher;
    private readonly ILogger<OutboxProcessor> _logger;

    public OutboxProcessor(IServiceScopeFactory scopeFactory, IEventPublisher eventPublisher, ILogger<OutboxProcessor> logger)
    {
        _scopeFactory = scopeFactory;
        _eventPublisher = eventPublisher;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using var scope = _scopeFactory.CreateScope();
            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Could shift any messages with errorcount above a threshold into a DLQ 

            var messages = await dbContext.OutboxMessages
                .Where(m => m.ProcessedOn == null)
                .OrderBy(m => m.OccurredOn)
                .Take(50)
                .ToListAsync(stoppingToken);

            foreach (var message in messages)
            {
                try
                {
                    //Converts the outbox message to a type and publishes it
                    await _eventPublisher.PublishAsync(message.Type, message.Payload, stoppingToken);
                    //Only mark the message as processed if it was successfully published
                    message.ProcessedOn = DateTime.UtcNow;
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Failed to process outbox message: {MessageId}", message.Id);
                    message.Error = ex.Message;
                    message.ErrorCount++;
                }
            }

            await dbContext.SaveChangesAsync(stoppingToken);
            await Task.Delay(5000, stoppingToken); // Poll every 5 seconds
        }
    }
}