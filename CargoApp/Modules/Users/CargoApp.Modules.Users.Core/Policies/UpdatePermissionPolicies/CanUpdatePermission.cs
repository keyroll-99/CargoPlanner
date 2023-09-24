using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Context;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.UpdatePermissionPolicies;

internal sealed class CanUpdatePermission : IPolicy<UpdatePermissionCommand>
{
    private readonly IContext _context;

    public CanUpdatePermission(IContext context)
    {
        _context = context;
    }

    public string ErrorMessage => "You cannot update user permission";
    public int StatusCode => StatusCodes.Status401Unauthorized;

    public bool IsApplicable(UpdatePermissionCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(UpdatePermissionCommand model)
    {
        return ValueTask.FromResult(_context.IdentityContext.HasPermission(PermissionEnum.Workers));
    }
}