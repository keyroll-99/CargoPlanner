using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Abstraction.Services;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Users.Core.Repositories;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;

namespace CargoApp.Modules.Users.Core.Events;

internal sealed class EmployeeFiredEventConsumer : IEventConsumer<EmployeeFiredEvent>
{
    private IServiceProvider _serviceProvider;

    public EmployeeFiredEventConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Process(EmployeeFiredEvent @event)
    {
        var userRepository = await _serviceProvider.GetService<IUserRepository>();
        
        var user = await userRepository.GetByEmployeeId(@event.EmployeeId);
        if(user is not null)
        {
            user.IsActive = false;
            await userRepository.UpdateAsync(user);
        }
    }
}