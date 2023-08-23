using CargoApp.Core.Abstraction.Enums;
using CargoApp.Modules.Users.Core.Entities;

namespace CargoApp.Modules.Users.Core.Commands;

public record UpdatePermissionCommand(PermissionEnum Permission, Guid UserId)
{
    public bool AddPermission { get; set; }
};