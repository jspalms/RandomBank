namespace Accounts.Domain.Interfaces;

public interface IEventPublisher<in TMessage> where TMessage : class
{
    Task PublishAsync(TMessage message);
}