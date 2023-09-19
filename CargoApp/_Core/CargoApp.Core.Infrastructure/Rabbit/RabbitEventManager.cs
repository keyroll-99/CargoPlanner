using System.Text;
using System.Text.Json;
using CargoApp.Core.Abstraction.QueueMessages;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CargoApp.Core.Infrastructure.Rabbit;

internal class RabbitEventManager : IEventManager
{
    private readonly RabbitFactory _rabbitFactory;
    private readonly ILogger _logger;

    public RabbitEventManager(RabbitFactory rabbitFactory, ILogger logger)
    {
        _rabbitFactory = rabbitFactory;
        _logger = logger;
    }

    public void PublishEvent<T>(T @event)
    {
        var factory = new ConnectionFactory { HostName = "localhost" };
        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);

        var message = "dupa";
        var body = Encoding.UTF8.GetBytes(message);
        channel.BasicPublish(exchange: "logs",
            routingKey: string.Empty,
            basicProperties: null,
            body: body);
        _logger.Information($" [x] Sent {message}");

    }

    public void RegisterConsumer<T>(IEventConsumer<T> consumerClass)
        where T: class
    {
        using var connection = _rabbitFactory.ConnectionFactory.CreateConnection();
        using var channel = connection.CreateModel();
        var eventType = typeof(T);

        channel.ExchangeDeclare(exchange: "CargoApp", type: ExchangeType.Topic);

        var queueName = channel.QueueDeclare().QueueName;
        
        channel.QueueBind(
            queue: queueName,
            exchange: "CargoApp",
            routingKey: eventType.FullName
        );

        var consumer = new EventingBasicConsumer(channel);

        consumer.Received += (model, ea) =>
        {
            var body = ea.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            var routingKey = ea.RoutingKey;
            consumerClass.Process(null as T);
        };

        channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);
    }
}