using Confluent.Kafka;
using System.Text.Json;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SharedKernel.IntegrationEvents;
using Users.Application.Interfaces;
using Users.Infrastructure.Configuration;

namespace Users.Infrastructure.Events.IntegrationEvents;

class KafkaEventBus(IServiceProvider serviceProvider, ILogger<KafkaEventBus> logger) : IEventBus
{
    private readonly ProducerConfig _producerConfig = serviceProvider.GetRequiredService<ProducerConfig>();
    private readonly ConsumerConfig _consumerConfig = serviceProvider.GetRequiredService<ConsumerConfig>();
    private readonly KafkaConsumerOptions _consumerOptions = serviceProvider.GetRequiredService<IOptions<KafkaConsumerOptions>>().Value;

    // How can I exposure being able to publish with or without a key without leaking the implementation details?

    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {
        using var producer = new ProducerBuilder<Null, string>(_producerConfig).Build();
        //Could have more granular topics e.g. one for each event type
        //var topic = @event.GetType().Name;
        var topic = "users-events";
        var payload = JsonSerializer.Serialize(@event);

        await producer.ProduceAsync(topic, new Message<Null, string> { Value = payload }, cancellationToken);
    }

    public async Task ConsumeTopicsAsync(CancellationToken cancellationToken)
    {
        var topics = _consumerOptions.Topics;
        
        logger.LogInformation("KafkaEventBus: Consuming topics: {Topics}", string.Join(", ", topics));
        //can use the _consumerConfig from DI
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
            PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky,
            SecurityProtocol = SecurityProtocol.Plaintext
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
                    
                    logger.LogInformation("KafkaEventBus: Consuming topic: {Topic}", consumeResult.Topic);

                    var baseEvent = JsonSerializer.Deserialize<IIntegrationEvent>(consumeResult.Message.Value) ?? throw new Exception();
                    var eventType = Type.GetType(baseEvent.EventTypeName) ?? throw new Exception();
                    logger.LogInformation("KafkaEventBus: Consumed Event of type: {EventType}", eventType);

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
