using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using Result.ApiResult;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal sealed class PermissionTools : IPermissionTools
{
    private readonly IEnumerable<IPolicy<UpdatePermissionCommand>> _policies;
    private readonly IUserRepository _userRepository;

    public PermissionTools(IEnumerable<IPolicy<UpdatePermissionCommand>> policies, IUserRepository userRepository)
    {
        _policies = policies;
        _userRepository = userRepository;
    }

    public async Task<ApiResult> AddPermission(UpdatePermissionCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return "User doesn't exists";
        }

        var result = await _policies.UsePolicies(command);
        
        await result.OnSuccessAsync((_) => AddPermission(user, command.Permission));

        return result;
    }

    public async Task<ApiResult> RemovePermission(UpdatePermissionCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return "User doesn't exists";
        }

        var result = await _policies.UsePolicies(command);
        
        await result.OnSuccessAsync((_) => RemovePermission(user, command.Permission));
        return result;
    }

    private async Task AddPermission(User user, PermissionEnum permission)
    {
        user.PermissionMask |= permission;
        await _userRepository.UpdateAsync(user);
    }

    private async Task RemovePermission(User user, PermissionEnum permission)
    {
        user.PermissionMask &= ~(permission);
        await _userRepository.UpdateAsync(user);
    }
}