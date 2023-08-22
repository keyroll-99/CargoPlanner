using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.UpdatePermissionPolicies;

internal class CanRemovePermissionToUserPolicy : IPolicy<UpdatePermissionCommand>
{
    public string ErrorMessage => "User has already this policy";
    public int StatusCode => StatusCodes.Status400BadRequest;

    private readonly IUserRepository _userRepository;

    public CanRemovePermissionToUserPolicy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }
    
    public bool CanBeApplied(UpdatePermissionCommand model)
        => !model.AddPermission;

    public async ValueTask<bool> IsValidAsync(UpdatePermissionCommand model)
    {
        var user = await _userRepository.GetByIdAsync(model.UserId);
        return
            !user?.PermissionMask.HasFlag(model.Permission) ??
            true; // if a user is null it returns true, because this policy checks only, whether does user has permission
    }
}