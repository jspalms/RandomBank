using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Users.Application.Interfaces;
using Users.Infrastructure.Configuration;
using Users.Infrastructure.Events.IntegrationEvents;

namespace Users.Infrastructure.Extensions;

public static class KafkaConfigurationExtensions
{
    public static IServiceCollection AddKafkaProducer(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddOptions<KafkaProducerOptions>()
            .Bind(configuration.GetSection("Kafka:ProducerOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();
        
        services.AddOptions<KafkaConsumerOptions>()
            .Bind(configuration.GetSection("Kafka:ConsumerOptions"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        // Register ProducerConfig
        services.AddSingleton(provider =>
        {
            var kafkaProducerOptions = provider.GetRequiredService<IOptions<KafkaProducerOptions>>().Value;
            return new ProducerConfig
            {
                BootstrapServers = kafkaProducerOptions.BootstrapServers,
                ClientId = kafkaProducerOptions.ClientId,
                SaslUsername = kafkaProducerOptions.SaslUsername,
                SaslPassword = kafkaProducerOptions.SaslPassword,
                SecurityProtocol = SecurityProtocol.Plaintext,
                SaslMechanism = SaslMechanism.Plain,
                Acks = Acks.All,
                EnableIdempotence = true
            };
        });
        
        // Register ConsumerConfig
        services.AddSingleton(provider =>
        {
            var kafkaConsumerOptions = provider.GetRequiredService<IOptions<KafkaConsumerOptions>>().Value;
            return new ConsumerConfig
            {
                BootstrapServers = kafkaConsumerOptions.BootstrapServers,
                GroupId = kafkaConsumerOptions.GroupId,
                EnableAutoOffsetStore = false,
                EnableAutoCommit = true,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnablePartitionEof = true,
                PartitionAssignmentStrategy = PartitionAssignmentStrategy.CooperativeSticky,
                AllowAutoCreateTopics = true
            };
        });

        services.AddSingleton<IEventBus, KafkaEventBus>();
        return services;
    }
    
}
