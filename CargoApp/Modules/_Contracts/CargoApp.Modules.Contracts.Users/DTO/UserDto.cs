using CargoApp.Core.Abstraction.Enums;

namespace CargoApp.Modules.Contracts.Users.DTO;

public record UserDto(Guid Id, string Email, bool IsActive, PermissionEnum PermissionEnum);

public static class UserDtoExtensions
{
    public static bool HasPermission(this UserDto user, PermissionEnum permission)
        => user.PermissionEnum.HasFlag(permission);
}