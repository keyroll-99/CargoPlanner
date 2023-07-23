using CargoApp.Core.Abstraction.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Security;

namespace CargoApp.Modules.Users.Core.Services;

internal class AuthService : IAuthService
{
    private readonly IEnumerable<IPolicy<CreateUserCommand>> _createUserPolicy;
    private readonly PasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public AuthService(
        IUserRepository userRepository,
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicy,
        PasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _createUserPolicy = createUserPolicy;
        _passwordManager = passwordManager;
    }

    public async Task<Match<UserDto, string>> CreateUserAsync(CreateUserCommand createUserCommand)
    {
        foreach (var policy in _createUserPolicy.Where(x => x.CanBeApplied(createUserCommand)))
            if (!await policy.IsValid(createUserCommand))
                return policy.ErrorMessage;

        var model = new User
        {
            Email = createUserCommand.Email,
            Id = Guid.NewGuid(),
            Password = _passwordManager.Hash(createUserCommand.Password),
            IsActive = true
        };
        await _userRepository.CreateAsync(model);

        return new UserDto(model.Id, model.Email, model.IsActive);
    }
}