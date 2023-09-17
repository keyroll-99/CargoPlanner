using System.Text;
using System.Text.Json;
using CargoApp.Core.Abstraction.QueueMessages;
using RabbitMQ.Client;

namespace CargoApp.Core.Infrastructure.Rabbit;

public class RabbitEventManager : IEventManager
{
    private readonly RabbitFactory _rabbitFactory;

    public RabbitEventManager(RabbitFactory rabbitFactory)
    {
        _rabbitFactory = rabbitFactory;
    }

    public void PublishEvent<T>(T @event)
    {
        using var connection = _rabbitFactory.ConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var eventType = typeof(T);
        var queueName = eventType.FullName;

        channel.QueueDeclare(queue: queueName);

        var serializedMessage = JsonSerializer.Serialize(@event);
        var body = Encoding.UTF8.GetBytes(serializedMessage);
        
        channel.BasicPublish(exchange: string.Empty, routingKey: queueName, body: body);
    }
}