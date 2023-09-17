using CargoApp.Core.Abstraction.QueueMessages;
using Serilog;

namespace CargoApp.Modules.Users.Core.Events;

public class EmployeeCreateEventConsumer : IEventConsumer<EmployeeCreateEventConsumer>
{
    private readonly ILogger _logger;

    public EmployeeCreateEventConsumer(ILogger logger)
    {
        _logger = logger;
    }

    public Task ProcessEvent(EmployeeCreateEventConsumer @event)
    {
        _logger.Information($"Consume event {typeof(EmployeeCreateEventConsumer).FullName}");
        return Task.CompletedTask;
    }
}