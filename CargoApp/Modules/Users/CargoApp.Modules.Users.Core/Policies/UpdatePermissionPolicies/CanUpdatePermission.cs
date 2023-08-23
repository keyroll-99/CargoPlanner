using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Abstraction.Enums;
using CargoApp.Core.Infrastructure.Context;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.UpdatePermissionPolicies;

public class CanUpdatePermission : IPolicy<UpdatePermissionCommand>
{
    private readonly IContext _context;

    public CanUpdatePermission(IContext context)
    {
        _context = context;
    }

    public string ErrorMessage => "You cannot update user permission";
    public int StatusCode => StatusCodes.Status401Unauthorized;

    public bool CanBeApplied(UpdatePermissionCommand model)
        => true;

    public async ValueTask<bool> IsValidAsync(UpdatePermissionCommand model)
    {
        return _context.IdentityContext.HasPermission(PermissionEnum.Workers);
    }
}