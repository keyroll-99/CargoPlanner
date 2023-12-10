using CargoApp.Modules.Contracts.Users.DTO;
using Result.ApiResult;

namespace CargoApp.Modules.Contracts.Users;

public interface IUser
{
    Task<ApiResult<UserDto, string>> GetUserByIdAsync(Guid id);
}