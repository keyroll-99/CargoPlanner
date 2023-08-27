using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.CreateUserCommandPolicies;

internal class PasswordStrengthPolicy : IPolicy<CreateUserCommand>
{
    public string ErrorMessage => "Password is not strong enough";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(CreateUserCommand model)
    {
        return true;
    }

    public ValueTask<bool> IsValidAsync(CreateUserCommand model)
    {
        return ValueTask.FromResult(model.Password.Length >= 6);
    }
}