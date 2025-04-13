using System.Text.Json;
using Accounts.Application.Interfaces;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using SharedKernel.IntegrationEvents;

namespace Accounts.Infrastructure.Events.IntegrationEvents;

public class KafkaEventBus(ProducerConfig producerConfig, ServiceProvider serviceProvider) : IEventBus
{
    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
        using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        //Could have more granular topics e.g. one for each event type
        //var topic = @event.GetType().Name;
        var topic = "users-events";
        var payload = JsonSerializer.Serialize(@event);

        await producer.ProduceAsync(topic, new Message<Null, string> { Value = payload }, cancellationToken);
    }

     public async Task ConsumeTopicsAsync(List<string> topics, CancellationToken cancellationToken)
    {
            var config = new ConsumerConfig
            {
                BootstrapServers = "localhost:9092",
                GroupId = "random_users",
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config)
                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .Build();
            
            consumer.Subscribe(topics);
            
            try
            {
                while (!cancellationToken.IsCancellationRequested)
                {
                    try
                    {
                        var consumeResult = consumer.Consume(cancellationToken);
                        if (consumeResult.IsPartitionEOF)
                        {
                            continue;
                        }

                        var baseEvent = JsonSerializer.Deserialize<IIntegrationEvent>(consumeResult.Message.Value) ?? throw new Exception();
                        var eventType = Type.GetType(baseEvent.EventTypeName) ?? throw new Exception();

                        var handlerType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                        dynamic handler = serviceProvider.GetService(handlerType);
                        if (handler != null)
                        {
                            await handler.HandleAsync(baseEvent, cancellationToken);
          
                        }
                    }
                    catch (ConsumeException e)
                    {
                        Console.WriteLine($"Error occurred: {e.Error.Reason}");
                    }
                }
            }
            catch (OperationCanceledException)
            {
                consumer.Close();
            }
    }
}