using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.DTO;

namespace CargoApp.Modules.Contracts.User;

public interface IUser
{
    Task<Result<UserDto, string>> GetUserByIdAsync(Guid id);
}