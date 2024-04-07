using Crud.RabbitMQ.Bus.RabbitConfig;
using EasyNetQ;
using EasyNetQ.Topology;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace Crud.RabbitMQ.Bus;

public class MessageBus : IMessageBus
{
    private IBus _bus;
    private IAdvancedBus _advancedBus;

    private readonly string _connectionString;

    public MessageBus(string connectionString)
    {
        _connectionString = connectionString;
        TryConnection();
        ConfigureRabbitMq();
    }

    private void ConfigureRabbitMq()
    {
        try
        {
            #region Exchanges
            var cadastroProdutoExchange = _advancedBus.ExchangeDeclare(ExchangesNames.NewCadastroProduto, ExchangeType.Topic);
            var cadastroCategoriaExchange = _advancedBus.ExchangeDeclare(ExchangesNames.NewCadastroCategoria, ExchangeType.Topic);
            #endregion

            #region Queues
            var cadastroProdutoFila = _advancedBus.QueueDeclare(QueuesNames.New_CadastroProduto);
            var cadastroCategoriaFila = _advancedBus.QueueDeclare(QueuesNames.New_CadastroCategoria);
            #endregion

            #region Binds
            _advancedBus.Bind(cadastroProdutoExchange, cadastroCategoriaFila, RountingKeysNames.Produto);
            _advancedBus.Bind(cadastroCategoriaExchange, cadastroCategoriaFila, RountingKeysNames.Categoria);
            #endregion
        }
        catch (Exception ex)
        {
            
            throw new Exception(ex.Message);
        }


    }

    private void TryConnection()
    {
        if (IsConnected)
            return;

        var policy = Policy.Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetry(3, retryAttempt =>
            TimeSpan.FromSeconds(Math.Pow(10, retryAttempt)));

        policy.Execute(() =>
        {
            _bus = RabbitHutch.CreateBus(_connectionString);
            _advancedBus = _bus.Advanced;
            _advancedBus.Disconnected += onDisconnect;
        });
    }

    private void onDisconnect(object? sender, EventArgs e)
    {
        var policy = Policy.Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .RetryForever();
        policy.Execute(TryConnection);
    }

    public bool IsConnected => _bus?.Advanced?.IsConnected ?? false;

    public IAdvancedBus AdvancedBus => _bus?.Advanced;

    public void Dispose() => _bus?.Dispose();

    public void Publish<T>(T message) where T : class
    {
        TryConnection();
        _bus.PubSub.Publish(message);
    }

    public void Publish<T>(T message, string topic) where T : class
    {
        TryConnection();
        _bus.PubSub.Publish(message, topic);
    }

    public async Task PublishAsync<T>(T message) where T : class
    {
        TryConnection();
        await _bus.PubSub.PublishAsync(message);
    }

    public async Task PublishAsync<T>(T message, string topic) where T : class
    {
        TryConnection();
        await _bus.PubSub.PublishAsync(message, topic);
    }

    public void Subscribe<T>(string subscribeId, Action<T> onMessage) where T : class
    {
        TryConnection();
        _bus.PubSub.Subscribe(subscribeId, onMessage);
    }

    public void Subscribe<T>(string subscribeId, Action<T> onMessage, string topic, string queueName) where T : class
    {
        TryConnection();
        _bus.PubSub.Subscribe(subscribeId, onMessage, x => x.WithTopic(topic).WithQueueName(queueName));
    }

    public async Task SubscribeAsync<T>(string subscribeId, Action<T> onMessage) where T : class
    {
        TryConnection();
        await _bus.PubSub.SubscribeAsync(subscribeId, onMessage);
    }

    public async Task SubscribeAsync<T>(string subscribeId, Action<T> onMessage, string topic, string queueName) where T : class
    {
        TryConnection();
        await _bus.PubSub.SubscribeAsync(subscribeId, onMessage, x => x.WithTopic(topic).WithQueueName(queueName));
    }
}
