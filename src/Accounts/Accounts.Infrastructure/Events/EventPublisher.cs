namespace Accounts.Infrastructure.Events;

using Domain.IntegrationEvents;
using Domain.Interfaces;
using MassTransit;

public class EventPublisher<T>: IEventPublisher<T> where T : class
{
    private readonly ITopicProducer<AccountOpenedIntegrationEvent> _topicProducer;
    
    public EventPublisher(ITopicProducer<AccountOpenedIntegrationEvent> topicProducer)
    {
        _topicProducer = topicProducer;
    }
    
    public Task PublishAsync(T message)
    {
        return message switch
        {
            AccountOpenedIntegrationEvent accountCreatedEvent => _topicProducer.Produce(accountCreatedEvent),
            _ => throw new ArgumentException($"Unsupported message type: {message.GetType().Name}")
        };
    }
}