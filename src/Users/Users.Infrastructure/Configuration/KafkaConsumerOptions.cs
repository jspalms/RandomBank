namespace Users.Infrastructure.Configuration;

public class KafkaConsumerOptions
{
    public required List<string> Topics { get; set; }
    public required string GroupId { get; set; }
    public required string BootstrapServers { get; set; }
}