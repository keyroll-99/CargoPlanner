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
    
    public RabbitEventManager(IModel chanel, ILogger logger)
    {
        _logger = logger;
        _chanel = chanel;
    }

    public void PublishEvent<T>(T @event)
    {
        _chanel.QueueDeclare(typeof(T).FullName, true, false, false);
        var properties = _chanel.CreateBasicProperties();
        properties.Persistent = true;
        
        var message = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(message);
        _chanel.BasicPublish(string.Empty, typeof(T).FullName, null, body);
    }

    public async Task ReceiveAsync<T>(Func<T, Task> action)
    {
        _chanel.QueueDeclare(typeof(T).FullName, true, false, false);
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