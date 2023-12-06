using CargoApp.Modules.Users.Core.Commands;
using Result.ApiResult;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IPasswordRecoveryService
{
    Task<ApiResult> InitPasswordRecovery(InitPasswordRecoveryCommand command);
    Task<ApiResult> IsRecoveryKeyValid(string recoveryKey);
    Task<ApiResult> ChangePassword(Guid recoveryKey, ChangePasswordCommand command);
}