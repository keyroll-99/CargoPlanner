using RabbitMQ.Client;

namespace CargoApp.Core.Infrastructure.Rabbit;

public class RabbitFactory
{
    public ConnectionFactory ConnectionFactory { get; private set; }
    
    
    public RabbitFactory(string hostName)
    {
        ConnectionFactory = new ConnectionFactory
        {
            HostName = hostName,
        };
    }
}