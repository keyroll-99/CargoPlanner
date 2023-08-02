namespace CargoApp.Modules.Users.Core.DTO;

public record UserDto(Guid Id, string Email, bool IsActive);