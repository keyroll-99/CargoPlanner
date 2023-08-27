using CargoApp.Core.Infrastructure.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal class PermissionTools : IPermissionTools
{
    private readonly IEnumerable<IPolicy<UpdatePermissionCommand>> _policies;
    private readonly IUserRepository _userRepository;

    public PermissionTools(IEnumerable<IPolicy<UpdatePermissionCommand>> policies, IUserRepository userRepository)
    {
        _policies = policies;
        _userRepository = userRepository;
    }

    public async Task<Result> AddPermission(UpdatePermissionCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return "User doesn't exists";
        }

        var result = await _policies.UsePolicies(command);
        return await result.Match(
            () => AddPermission(user, command.Permission),
            (onErrorResult) => Task.FromResult<Result>(onErrorResult));
    }

    public async Task<Result> RemovePermission(UpdatePermissionCommand command)
    {
        var user = await _userRepository.GetByIdAsync(command.UserId);
        if (user is null)
        {
            return "User doesn't exists";
        }

        var result = await _policies.UsePolicies(command);
        return await result.Match(
            () => RemovePermission(user, command.Permission),
            (onErrorResult) => Task.FromResult<Result>(onErrorResult));
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