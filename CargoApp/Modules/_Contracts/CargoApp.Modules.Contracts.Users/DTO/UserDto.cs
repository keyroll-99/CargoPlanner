using CargoApp.Core.ShareCore.Enums;

namespace CargoApp.Modules.Contracts.Users.DTO;

public record UserDto(Guid Id, string Email, bool IsActive, PermissionEnum Permission);

public static class UserDtoExtensions
{
    public static bool HasPermission(this UserDto user, PermissionEnum permission)
        => user.Permission.HasFlag(permission);
}