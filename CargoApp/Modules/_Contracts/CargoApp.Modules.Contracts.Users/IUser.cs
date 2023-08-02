using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Contracts.Users.DTO;

namespace CargoApp.Modules.Contracts.Users;

public interface IUser
{
    Task<Result<UserDto, string>> GetUserByIdAsync(Guid id);
}