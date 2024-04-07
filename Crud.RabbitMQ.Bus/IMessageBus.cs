using EasyNetQ;

namespace Crud.RabbitMQ.Bus;

public interface IMessageBus : IDisposable
{
    bool IsConnected { get; }

    IAdvancedBus AdvancedBus { get; }

    void Publish<T>(T message) where T : class;
    void Publish<T>(T message, string topic) where T : class;

    Task PublishAsync<T>(T message) where T : class;
    Task PublishAsync<T>(T message, string topic) where T : class;

    void Subscribe<T>(string subscribeId, Action<T> onMessage) where T : class;
    void Subscribe<T>(string subscribeId, Action<T> onMessage, string topic, string queueName) where T : class;

    Task SubscribeAsync<T>(string subscribeId, Action<T> onMessage) where T : class;
    Task SubscribeAsync<T>(string subscribeId, Action<T> onMessage, string topic, string queueName) where T : class;

}
