using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IAuthService
{
    public Task<Result<UserDto, string>> CreateUserAsync(CreateUserCommand createUserCommand);
    public Task<Result<JsonWebToken, string>> SignInAsync(SingInCommand singInCommand);
}