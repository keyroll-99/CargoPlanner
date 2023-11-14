using CargoApp.Core.Abstraction.Mail;
using CargoApp.Core.Infrastructure.Metadata;
using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.EmailTemplates.PasswordRecovery;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using Microsoft.AspNetCore.Identity;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class PasswordRecoveryService : IPasswordRecoveryService
{
    private readonly IPasswordRecoveryRepository _passwordRecoveryRepository;
    private readonly IClock _clock;
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IMailManager _mailManager;
    private readonly Metadata _metadata;
    private readonly IEnumerable<IPolicy<ChangePasswordCommand>> _changePasswordPolicies;

    public PasswordRecoveryService(
        IPasswordRecoveryRepository passwordRecoveryRepository,
        IUserRepository userRepository,
        IClock clock,
        IMailManager mailManager,
        Metadata metadata,
        IPasswordHasher<User> passwordHasher,
        IEnumerable<IPolicy<ChangePasswordCommand>> changePasswordPolicies)
    {
        _passwordRecoveryRepository = passwordRecoveryRepository;
        _userRepository = userRepository;
        _clock = clock;
        _mailManager = mailManager;
        _metadata = metadata;
        _passwordHasher = passwordHasher;
        _changePasswordPolicies = changePasswordPolicies;
    }

    public async Task<CargoApp.Core.Infrastructure.Response.Result> InitPasswordRecovery(
        InitPasswordRecoveryCommand command)
    {
        var user = await _userRepository.GetByEmailAsync(command.Email);
        if (user is null)
        {
            return CargoApp.Core.Infrastructure.Response.Result.Fail("User not found");
        }

        var passwordRecovery = PasswordRecovery.CreatePasswordRecovery(user, _clock);
        await _passwordRecoveryRepository.AddAsync(passwordRecovery);
        var passwordRecoveryMail = new PasswordRecoveryMail(
            _metadata.FrontUrl,
            passwordRecovery.Id.ToString(),
            _metadata.ContactMail);

        await _mailManager.SendMailAsync(
            MailModel.CreateModel(
                user.Email,
                "Password recovery"),
            passwordRecoveryMail);
        return CargoApp.Core.Infrastructure.Response.Result.Success();
    }

    public async Task<CargoApp.Core.Infrastructure.Response.Result> IsRecoveryKeyValid(string recoveryKey)
    {
        if (!Guid.TryParse(recoveryKey, out var recoveryGuid))
        {
            return CargoApp.Core.Infrastructure.Response.Result.Fail("Invalid recovery key");
        }

        var recoveryModel = await _passwordRecoveryRepository.GetByIdAsync(recoveryGuid);

        if (recoveryModel is null)
        {
            return CargoApp.Core.Infrastructure.Response.Result.Fail("Invalid recovery key");
        }

        return recoveryModel.IsValid(_clock)
            ? CargoApp.Core.Infrastructure.Response.Result.Success()
            : CargoApp.Core.Infrastructure.Response.Result.Fail("Invalid recovery key");
    }

    public async Task<CargoApp.Core.Infrastructure.Response.Result> ChangePassword(Guid recoveryKey,
        ChangePasswordCommand command)
    {
        var changePasswordPoliciesResult = await _changePasswordPolicies.UsePolicies(command);

        if (!changePasswordPoliciesResult.IsSuccess)
        {
            return CargoApp.Core.Infrastructure.Response.Result.Fail(changePasswordPoliciesResult.Error!);
        }

        var recoveryModel = await _passwordRecoveryRepository.GetByIdAsync(recoveryKey);
        if (recoveryModel is null)
        {
            return CargoApp.Core.Infrastructure.Response.Result.Fail("Invalid recovery key");
        }

        var user = recoveryModel.User;

        user.Password = _passwordHasher.HashPassword(null, command.Password);
        recoveryModel.IsUsed = true;

        await _userRepository.UpdateAsync(user);
        await _passwordRecoveryRepository.UpdateAsync(recoveryModel);

        return CargoApp.Core.Infrastructure.Response.Result.Success();
    }
}