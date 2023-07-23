﻿using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies;

internal class PasswordStrengthPolicy : IPolicy<CreateUserCommand>
{
    public string ErrorMessage => "Password is not strong enough";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool CanBeApplied(CreateUserCommand model)
    {
        return true;
    }

    public ValueTask<bool> IsValid(CreateUserCommand model)
    {
        return ValueTask.FromResult(model.Password.Length >= 6);
    }
}