using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Context;
using CargoApp.Core.ShareCore.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Auth;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class RequirePermissionAttribute : AuthorizeAttribute, IAuthorizationFilter
{
    private readonly PermissionEnum _requiredPermission;

    public RequirePermissionAttribute(PermissionEnum requiredPermission)
    {
        _requiredPermission = requiredPermission;
    }

    public void OnAuthorization(AuthorizationFilterContext context)
    {
        var userContext = context.HttpContext.RequestServices.GetService<IContext>();
        if (userContext?.IdentityContext.HasPermission(_requiredPermission) == true)
        {
            return;
        }

        context.Result = new ObjectResult("User doesn't have permission")
        {
            StatusCode = StatusCodes.Status401Unauthorized
        };
    }
}