using CargoApp.Core.Abstraction.Mail;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IPasswordRecoveryRepository _passwordRecoveryRepository;
    private readonly IClock _clock;
    private readonly IUserRepository _userRepository;
    private readonly IMailManager _mailManager;

    public PasswordRecoveryService(
        IPasswordRecoveryRepository passwordRecoveryRepository,
        IUserRepository userRepository,
        IClock clock,
        IMailManager mailManager)
    {
        _passwordRecoveryRepository = passwordRecoveryRepository;
        _userRepository = userRepository;
        _clock = clock;
        _mailManager = mailManager;
    }

    public async Task<Result> InitPasswordRecovery(InitPasswordRecoveryCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            return Result.Fail("User not found");
        }

        var passwordRecovery = PasswordRecovery.CreatePasswordRecovery(user, _clock);
        await _passwordRecoveryRepository.AddAsync(passwordRecovery);
        await _mailManager.SendMailAsync(MailModel.CreateModel($"recovery link {passwordRecovery.Id}", user.Email,
            "Password recovery"));
        return Result.Success();
    }
}