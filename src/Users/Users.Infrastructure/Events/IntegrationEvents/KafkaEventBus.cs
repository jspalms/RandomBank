using Confluent.Kafka;
using Microsoft.Extensions.Options;
using SharedKernel.Domain.Interfaces;
using System.Text.Json;
using SharedKernel.IntegrationEvents;
using Users.Application.Interfaces;

namespace Users.Infrastructure.Events.IntegrationEvents;

class KafkaEventBus(ProducerConfig producerConfig) : IEventBus
{

    // How can I exposure being able to publish with or without a key without leaking the implementation details?

    public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : IIntegrationEvent
    {

        using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        //Could have more granular topics e.g. one for each event type
        //var topic = @event.GetType().Name;
        var topic = "users-events";
        var payload = JsonSerializer.Serialize(@event);

        await producer.ProduceAsync(topic, new Message<Null, string> { Value = payload }, cancellationToken);
    }
}
