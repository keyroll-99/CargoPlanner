using CargoApp.Core.Abstraction.Auth;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Commands;
using Result.ApiResult;

namespace CargoApp.Modules.Users.Core.Services.Abstract;

public interface IAuthService
{
    public Task<ApiResult<UserDto>>CreateUserAsync(CreateUserCommand createUserCommand);
    public Task<ApiResult<JsonWebToken>> SignInAsync(SingInCommand singInCommand);
}