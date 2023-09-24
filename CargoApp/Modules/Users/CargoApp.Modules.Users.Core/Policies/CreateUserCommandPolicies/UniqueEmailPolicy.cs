using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Policies.CreateUserCommandPolicies;

internal sealed class UniqueEmailPolicy : IPolicy<CreateUserCommand>
{
    private readonly IUserRepository _userRepository;


    public UniqueEmailPolicy(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public int StatusCode => StatusCodes.Status400BadRequest;

    public string ErrorMessage => "User with this email exists";

    public bool IsApplicable(CreateUserCommand model)
    {
        return true;
    }

    public async ValueTask<bool> IsValidAsync(CreateUserCommand model)
    {
        return !await _userRepository.ExistsByEmailAsync(model.Email);
    }
}