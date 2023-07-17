using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;

namespace CargoApp.Modules.Users.Core.Policies;

public class PasswordStrengthPolicy : IPolicy<CreateUserCommand>
{
    public bool CanBeApplied(CreateUserCommand model)
        => true;

    public bool IsValid(CreateUserCommand model)
        => model.Password.Length >= 6;
}