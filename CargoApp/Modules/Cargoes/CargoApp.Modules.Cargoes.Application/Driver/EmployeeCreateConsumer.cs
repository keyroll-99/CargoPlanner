using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Modules.Contracts.Events.Companies;

namespace CargoApp.Modules.Cargoes.Application.Driver;

public class EmployeeCreateConsumer : IEventConsumer<EmployeeCreateEvent>
{

    public Task Process(EmployeeCreateEvent @event)
    {
        throw new NotImplementedException();
    }
}