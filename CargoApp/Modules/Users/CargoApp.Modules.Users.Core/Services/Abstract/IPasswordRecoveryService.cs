using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IPasswordRecoveryService
{
    Task<Result> InitPasswordRecovery(InitPasswordRecoveryCommand command);
    Task<Result> IsRecoveryKeyValid(string recoveryKey);
    Task<Result> ChangePassword(Guid recoveryKey, ChangePasswordCommand command);
}