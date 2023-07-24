using System.Text.RegularExpressions;
using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;

namespace CargoApp.Modules.Users.Core.Services;

public interface IAuthService
{
    public Task<Result<UserDto, string>> CreateUserAsync(CreateUserCommand createUserCommand);
    public Task<Result<JsonWebToken, string>> SignInAsync(SingInCommand singInCommand);
}