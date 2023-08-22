using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IPermissionTools
{
    Task<Result> AddPermission(UpdatePermissionCommand command);
    Task<Result> RemovePermission(UpdatePermissionCommand command);
}