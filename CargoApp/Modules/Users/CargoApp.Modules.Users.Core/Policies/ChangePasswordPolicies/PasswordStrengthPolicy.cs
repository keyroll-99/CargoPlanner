using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.ChangePasswordPolicies;

internal sealed class PasswordStrengthPolicy : IPolicy<ChangePasswordCommand>
{
    public string ErrorMessage => "Password is not strong enough";
    public int StatusCode => StatusCodes.Status400BadRequest;

    public bool IsApplicable(ChangePasswordCommand model)
    {
        return true;
    }

    public ValueTask<bool> IsValidAsync(ChangePasswordCommand model)
    {
        return ValueTask.FromResult(model.Password.Length >= 6);
    }
}