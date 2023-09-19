using System.Text;
using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Rabbit;
using CargoApp.Modules.Contracts.Events.Companies;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using Serilog;

namespace CargoApp.Modules.Users.Core.Events;

public class Test : BackgroundService
{
    private RabbitFactory _rabbitFactory;
    private readonly ILogger _logger;

    public Test(RabbitFactory rabbitFactory, ILogger logger)
    {
        _rabbitFactory = rabbitFactory;
        _logger = logger;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        using PeriodicTimer timer = new(TimeSpan.FromSeconds(10));
        
        try
        {
            // while (await timer.WaitForNextTickAsync(stoppingToken))
            // {
            //     var factory = new ConnectionFactory { HostName = "localhost" };
            //     using var connection = factory.CreateConnection();
            //     using var channel = connection.CreateModel();
            //
            //     channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
            //
            //     // declare a server-named queue
            //     var queueName = channel.QueueDeclare().QueueName;
            //     channel.QueueBind(queue: queueName,
            //         exchange: "logs",
            //         routingKey: string.Empty);
            //
            //     _logger.Information(" [*] Waiting for logs.");
            //
            //     var consumer = new EventingBasicConsumer(channel);
            //     consumer.Received += (model, ea) =>
            //     {
            //         byte[] body = ea.Body.ToArray();
            //         var message = Encoding.UTF8.GetString(body);
            //         _logger.Information($" [x] {message}");
            //     };
            //     channel.BasicConsume(queue: queueName,
            //         autoAck: true,
            //         consumer: consumer);
            // }
        }
        catch (OperationCanceledException)
        {
            _logger.Information("Timed Hosted Service is stopping.");
        }
        

    }

    // public async Task StartAsync(CancellationToken cancellationToken)
    // {
    //     await using var scope = _serviceProvider.CreateAsyncScope();
    //
    //     var eventManager = scope.ServiceProvider.GetService<IEventManager>();
    //     var consumer = scope.ServiceProvider.GetService<EmployeeCreateEventConsumer>();
    //     
    //     eventManager.RegisterConsumer(consumer);
    // }
    //
    // public Task StopAsync(CancellationToken cancellationToken)
    //     => Task.CompletedTask;
}