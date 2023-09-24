using CargoApp.Core.Abstraction.QueueMessages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CargoApp.Core.Infrastructure.Rabbit;

public class RabbitEventConsumer<TProcessor ,TEvent> : BackgroundService
    where TEvent : class
    where TProcessor: IEventConsumer<TEvent>
{

    private readonly IEventConsumer<TEvent> _eventConsumer;
    private readonly IEventManager _eventManager;
    private readonly ILogger _logger;


    public RabbitEventConsumer(TProcessor eventConsumer, IEventManager eventManager, ILogger logger)
    {
        _eventConsumer = eventConsumer;
        _eventManager = eventManager;
        _logger = logger;
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _eventManager.ReceiveAsync<TEvent>(async (model) =>
        {
            try
            {
                await _eventConsumer.Process(model);
            }
            catch (System.Exception e)
            {
                _logger.Error(e, "Error while execute message {messagePath}", typeof(TEvent).FullName);
            }
        });

        return Task.CompletedTask;
    }
}