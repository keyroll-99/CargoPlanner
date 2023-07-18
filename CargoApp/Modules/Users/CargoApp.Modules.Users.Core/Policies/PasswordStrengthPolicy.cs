using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Policies;

public class PasswordStrengthPolicy : IPolicy<CreateUserCommand>
{
    public string ErrorMessage => "Password is not strong enough";

    public bool CanBeApplied(CreateUserCommand model)
        => true;

    public ValueTask<bool> IsValid(CreateUserCommand model)
        => ValueTask.FromResult(model.Password.Length >= 6);
}