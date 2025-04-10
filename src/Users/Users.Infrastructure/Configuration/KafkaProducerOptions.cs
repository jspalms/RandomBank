﻿namespace Users.Infrastructure.Configuration;

class KafkaProducerOptions
{
    // User-specific properties that you must set
    public string BootstrapServers { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
    public string ClientId { get; set; } = "RandomUsers";

    // Fixed properties
    public string SecurityProtocol { get;} = "SecurityProtocol.SaslSsl";
    public string SaslMechanism { get;} = "SaslMechanism.Plain";
    public string Acks { get;} = "Acks.All";
}
