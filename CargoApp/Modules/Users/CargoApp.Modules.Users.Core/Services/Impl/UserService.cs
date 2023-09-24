using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Contracts.Users;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Mappers;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal sealed class UserService : IUser
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<Result<UserDto, string>> GetUserByIdAsync(Guid id)
    {
        var dbModel = (await _userRepository.GetByIdAsync(id));
        return dbModel?.AsUserDto() ?? Result<UserDto, string>.Fail("User not found", StatusCodes.Status404NotFound);
    }
}