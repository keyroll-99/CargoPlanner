using CargoApp.Modules.Users.Core.Commands;
using Result.ApiResult;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IPermissionTools
{
    Task<ApiResult> AddPermission(UpdatePermissionCommand command);
    Task<ApiResult> RemovePermission(UpdatePermissionCommand command);
}