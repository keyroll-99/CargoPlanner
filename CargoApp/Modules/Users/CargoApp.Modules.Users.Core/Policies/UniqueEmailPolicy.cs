using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Repositories;

namespace CargoApp.Modules.Users.Core.Policies;

public class UniqueEmailPolicy : IPolicy<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;

    public UniqueEmailPolicy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public string ErrorMessage => "User with this email exists";

    public bool CanBeApplied(CreateUserCommand model)
        => true;

    public async ValueTask<bool> IsValid(CreateUserCommand model)
    {
        return !(await _userRepository.ExistsByEmail(model.Email));
    }
}