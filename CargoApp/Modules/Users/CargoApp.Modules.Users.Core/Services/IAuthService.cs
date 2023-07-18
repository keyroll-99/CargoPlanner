using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;

namespace CargoApp.Modules.Users.Core.Services;

public interface IAuthService
{
    public Task<UserDto> CreateUserAsync(CreateUserCommand createUserCommand);
}