using System.Security.Claims;
using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Abstraction.Enums;

namespace CargoApp.Core.Infrastructure.Context;

public class IdentityContext : IIdentityContext
{
    public bool IsAuthenticated { get; }
    public Guid Id { get; }
    public Dictionary<string, IEnumerable<string>> Claims { get; }
    public PermissionEnum Permissions { get; }

    public IdentityContext(ClaimsPrincipal principal)
    {
        IsAuthenticated = principal.Identity?.IsAuthenticated is true;
        Id = IsAuthenticated ? Guid.Parse(principal.Identity.Name) : Guid.Empty;
        Claims = principal.Claims.GroupBy(x => x.Type)
            .ToDictionary(x => x.Key, x => x.Select(c => c.Value.ToString()));

        Claims.TryGetValue(ClaimsConst.Permission, out IEnumerable<string>? permissions);
        var permission = permissions?.FirstOrDefault();
        if (permission is not null && long.TryParse(permission, out var parsedPermission))
        {
            Permissions = (PermissionEnum)parsedPermission;
        }
    }
}

public static class IdentityContextExtensions
{
    public static bool HasPermission(this IIdentityContext identityContext, PermissionEnum permissionEnum)
        => identityContext.Permissions.HasFlag(permissionEnum);
}