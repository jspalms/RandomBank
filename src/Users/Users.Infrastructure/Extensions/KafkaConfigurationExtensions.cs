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
            .Bind(configuration.GetSection("KafkaProducer"))
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
                SecurityProtocol = SecurityProtocol.SaslSsl,
                SaslMechanism = SaslMechanism.Plain,
                Acks = Acks.All,
                EnableIdempotence = true
            };
        });

        services.AddSingleton<IEventBus, KafkaEventBus>();
        return services;
    }
    
}
