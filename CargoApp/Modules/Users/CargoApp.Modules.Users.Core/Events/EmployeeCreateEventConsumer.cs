using CargoApp.Core.Abstraction.Mail;
using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Metadata;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Users.Core.EmailTemplates.NewUser;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using Serilog;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;

namespace CargoApp.Modules.Users.Core.Events;

internal sealed class EmployeeCreateEventConsumer : IEventConsumer<EmployeeCreateEvent>
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMailManager _mailManager;
    private readonly IClock _clock;
    private readonly Metadata _metadata;

    public EmployeeCreateEventConsumer(
        ILogger logger,
        IServiceProvider serviceProvider,
        IMailManager mailManager,
        IClock clock,
        Metadata metadata)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
        _mailManager = mailManager;
        _clock = clock;
        _metadata = metadata;
    }

    public async Task Process(EmployeeCreateEvent @event)
    {
        var userRepo = await _serviceProvider.GetService<IUserRepository>();
        // I don't like it, because now I have in two place this rules :(, maybe in the future I have to do Aggergate for this
        if (await userRepo.ExistsByEmailAsync(@event.Email))
        {
            _logger.Warning("Cannot create user for employee, because user with {email}, exists", @event.Email);
            return;
        }

        var addUserResult = await userRepo.AddAsync(@event);
        if (!addUserResult.IsSuccess)
        {
            _logger.Error("Cannot add user error {error}", addUserResult.Error);
            return;
        }

        var passwordRecoveryRepository = await _serviceProvider.GetService<IPasswordRecoveryRepository>();
        var user = addUserResult.SuccessModel;
        var recoveryModel = PasswordRecovery.CreatePasswordRecovery(user!.Id, _clock);
        //TODO: user try add two times
        await passwordRecoveryRepository.AddAsync(recoveryModel);

        await _mailManager.SendMailAsync(
            MailModel.CreateModel(user.Email, "Welcome in cargo app"),
            new WelcomeMail(_metadata.FrontUrl, recoveryModel.Id.ToString()));
    }
}