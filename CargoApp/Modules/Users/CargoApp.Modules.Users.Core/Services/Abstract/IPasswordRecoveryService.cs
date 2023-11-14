using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IPasswordRecoveryService
{
    Task<CargoApp.Core.Infrastructure.Response.Result> InitPasswordRecovery(InitPasswordRecoveryCommand command);
    Task<CargoApp.Core.Infrastructure.Response.Result> IsRecoveryKeyValid(string recoveryKey);
    Task<CargoApp.Core.Infrastructure.Response.Result> ChangePassword(Guid recoveryKey, ChangePasswordCommand command);
}