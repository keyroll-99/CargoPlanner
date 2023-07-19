using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;

namespace CargoApp.Modules.Users.Core.Services;

public interface IAuthService
{
    public Task<Match<UserDto, string>> CreateUserAsync(CreateUserCommand createUserCommand);
}