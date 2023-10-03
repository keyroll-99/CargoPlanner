using System.Reflection;
using CargoApp.Core.Abstraction.Mail;
using CargoApp.Core.Infrastructure.Mail;
using CargoApp.Core.Infrastructure.Metadata;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.EmailTemplates.PasswordRecovery;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using RazorLight;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IPasswordRecoveryRepository _passwordRecoveryRepository;
    private readonly IClock _clock;
    private readonly IUserRepository _userRepository;
    private readonly IMailManager _mailManager;
    private readonly Metadata _metadata;

    public PasswordRecoveryService(
        IPasswordRecoveryRepository passwordRecoveryRepository,
        IUserRepository userRepository,
        IClock clock,
        IMailManager mailManager,
        Metadata metadata)
    {
        _passwordRecoveryRepository = passwordRecoveryRepository;
        _userRepository = userRepository;
        _clock = clock;
        _mailManager = mailManager;
        _metadata = metadata;
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
        var dummyModel = new PasswordRecoveryMail(
            _metadata.FrontUrl,
            passwordRecovery.Id.ToString(),
            _metadata.FrontUrl);
        
        await _mailManager.SendMailAsync(
            MailModel.CreateModel(
                user.Email,
            "Password recovery"),
            dummyModel);
        return Result.Success();
    }
}