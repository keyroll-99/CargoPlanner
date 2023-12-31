﻿using CargoApp.Core.ShareCore.Enums;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.UpdatePermissionPolicies;

internal sealed class IsValidPermissionPolicy : IPolicy<UpdatePermissionCommand>
{
    public string ErrorMessage => "Invalid permission value";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(UpdatePermissionCommand model)
        => true;

    public ValueTask<bool> IsValidAsync(UpdatePermissionCommand model)
    {
        return new ValueTask<bool>(Enum.IsDefined(typeof(PermissionEnum), model.Permission));
    }
}