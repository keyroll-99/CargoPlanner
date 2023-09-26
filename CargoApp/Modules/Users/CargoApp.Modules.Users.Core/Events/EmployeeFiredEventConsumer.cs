using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Users.Core.Repositories;

namespace CargoApp.Modules.Users.Core.Events;

internal sealed class EmployeeFiredEventConsumer : IEventConsumer<EmployeeFiredEvent>
{
    private IRepositoryProvider _repositoryProvider;

    public EmployeeFiredEventConsumer(IRepositoryProvider repositoryProvider)
    {
        _repositoryProvider = repositoryProvider;
    }

    public async Task Process(EmployeeFiredEvent @event)
    {
        var userRepository = await _repositoryProvider.GetRepository<IUserRepository>();
        
        var user = await userRepository.GetByEmployeeId(@event.EmployeeId);
        if(user is not null)
        {
            user.IsActive = false;
            await userRepository.UpdateAsync(user);
        }
    }
}