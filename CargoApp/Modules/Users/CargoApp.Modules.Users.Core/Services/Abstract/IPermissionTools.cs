using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IPermissionTools
{
    Task<CargoApp.Core.Infrastructure.Response.Result> AddPermission(UpdatePermissionCommand command);
    Task<CargoApp.Core.Infrastructure.Response.Result> RemovePermission(UpdatePermissionCommand command);
}