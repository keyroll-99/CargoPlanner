using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Modules.Contracts.Events.Companies;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CargoApp.Modules.Users.Core.Events;

public class EmployeeCreateEventConsumer : IEventConsumer<EmployeeCreateEvent>
{
    private readonly ILogger _logger;

    public EmployeeCreateEventConsumer(ILogger logger)
    {
        _logger = logger;
    }

    public Task Process(EmployeeCreateEventConsumer @event)
    {
        _logger.Information("execute event");
        return Task.CompletedTask;
    }

    public Task Process(EmployeeCreateEvent @event)
    {
        _logger.Information("execute event");
        return Task.CompletedTask;
    }
}