using Confluent.Kafka;

namespace Users.Infrastructure.Configuration;

class KafkaProducerOptions
{
    // Application-specific properties
    public string BootstrapServers { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
    public string ClientId { get; set; } = "RandomUsers";
}
