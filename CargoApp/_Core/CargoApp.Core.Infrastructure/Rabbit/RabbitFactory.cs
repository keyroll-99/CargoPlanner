using CargoApp.Core.Abstraction.QueueMessages;
using RabbitMQ.Client;
using Serilog;

namespace CargoApp.Core.Infrastructure.Rabbit;

public static class RabbitFactory
{
    public static IEventManager CreateEventManager(string hostName, ILogger logger)
    {
        var factory = new ConnectionFactory
        {
            HostName = "localhost", 
            DispatchConsumersAsync = true
        };
        var connection = factory.CreateConnection();
        var channel = connection.CreateModel();

        return new RabbitEventManager(channel, logger);
    }
}