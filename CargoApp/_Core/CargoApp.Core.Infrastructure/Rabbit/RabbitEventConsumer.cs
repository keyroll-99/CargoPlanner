using CargoApp.Core.Abstraction.QueueMessages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CargoApp.Core.Infrastructure.Rabbit;

public class RabbitEventConsumer<TEvent> : BackgroundService
    where TEvent : class
{

    private readonly IEnumerable<IEventConsumer<TEvent>> _eventConsumers;
    private readonly IEventManager _eventManager;
    private readonly ILogger _logger;


    public RabbitEventConsumer(IEnumerable<IEventConsumer<TEvent>> eventConsumers, IEventManager eventManager, ILogger logger)
    {
        _eventConsumers = eventConsumers;
        _eventManager = eventManager;
        _logger = logger;
    }
    
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _eventManager.ReceiveAsync<TEvent>(async (model) =>
        {
            var tasks = new List<Task>();
            foreach (var eventConsumer in _eventConsumers)
            {
                try
                {
                    tasks.Add(eventConsumer.Process(model));
                }
                catch (System.Exception e)
                {
                    _logger.Fatal(e, "Error while execute message {messagePath}", typeof(TEvent).FullName);
                }
            }

            await Task.WhenAll(tasks);
        });

        return Task.CompletedTask;
    }
}