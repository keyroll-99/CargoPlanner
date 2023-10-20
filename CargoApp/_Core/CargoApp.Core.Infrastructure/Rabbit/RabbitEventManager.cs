using System.Text;
using System.Text.Json;
using CargoApp.Core.Abstraction.QueueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CargoApp.Core.Infrastructure.Rabbit;

internal class RabbitEventManager : IEventManager
{
    private readonly ILogger _logger;
    private readonly IModel _chanel;
    private readonly IDictionary<string, IModel> _exchangeName;
    
    public RabbitEventManager(IModel chanel, ILogger logger)
    {
        _logger = logger;
        _chanel = chanel;
        _exchangeName = new Dictionary<string, IModel>();
    }

    public void PublishEvent<T>(T @event)
    {
        _chanel.ExchangeDeclare(exchange: typeof(T).FullName, ExchangeType.Fanout);
        var properties = _chanel.CreateBasicProperties();
        properties.Persistent = true;
        
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);
        _chanel.BasicPublish(typeof(T).FullName, string.Empty, properties, body);
    }

    public async Task ReceiveAsync<T>(Func<T, Task> action)
    {
        if (!_exchangeName.ContainsKey(typeof(T).FullName))
        {
            _chanel.ExchangeDeclare(exchange: typeof(T).FullName, type: ExchangeType.Fanout);
            _exchangeName[typeof(T).FullName] = _chanel;
        }

        var queueName = _chanel.QueueDeclare().QueueName;
        
        _chanel.QueueBind(queueName, typeof(T).FullName, string.Empty);
        
        var consumer = new AsyncEventingBasicConsumer(_chanel);

        consumer.Received += async (@object, @event) =>
        {
            var json = Encoding.UTF8.GetString(@event.Body.Span);
            var item = JsonSerializer.Deserialize<T>(json);
            await action(item);
        };

        _chanel.BasicConsume(typeof(T).FullName, true, consumer);
        await Task.Yield();
    }
}