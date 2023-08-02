namespace CargoApp.Modules.Contracts.Users.DTO;

public record UserDto(Guid Id, string Email, bool IsActive);