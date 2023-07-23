using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies;

internal class UniqueEmailPolicy : IPolicy<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;


    public UniqueEmailPolicy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage => "User with this email exists";

    public bool CanBeApplied(CreateUserCommand model)
    {
        return true;
    }

    public async ValueTask<bool> IsValid(CreateUserCommand model)
    {
        return !await _userRepository.ExistsByEmail(model.Email);
    }
}