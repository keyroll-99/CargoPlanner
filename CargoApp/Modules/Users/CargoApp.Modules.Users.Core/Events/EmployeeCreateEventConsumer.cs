using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace CargoApp.Modules.Users.Core.Events;

internal sealed class EmployeeCreateEventConsumer : IEventConsumer<EmployeeCreateEvent>
{
    private readonly ILogger _logger;
    private readonly IRepositoryProvider _repositoryProvider;

    public EmployeeCreateEventConsumer(ILogger logger, IRepositoryProvider repositoryProvider)
    {
        _logger = logger;
        _repositoryProvider = repositoryProvider;
    }

    public async Task Process(EmployeeCreateEvent @event)
    {
        var userRepo = await _repositoryProvider.GetRepository<IUserRepository>();
        // I don't like it, because now I have in two place this rules :(, maybe in the future I have to do Aggergate for this
        if (await userRepo.ExistsByEmailAsync(@event.Email))
        {
            _logger.Warning("Cannot create user for employee, because user with {email}, exists", @event.Email);
            return;
        }

        await userRepo.AddAsync(@event);
        // TODO: send mail
    }
}