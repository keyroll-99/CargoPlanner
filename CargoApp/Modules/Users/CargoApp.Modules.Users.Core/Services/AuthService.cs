using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Security;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Users.Core.Services;

internal class AuthService : IAuthService
{
    private readonly IEnumerable<IPolicy<CreateUserCommand>> _createUserPolicy;
    private readonly PasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authManager;

    public AuthService(
        IUserRepository userRepository,
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicy,
        PasswordManager passwordManager, 
        IAuthManager authManager)
    {
        _userRepository = userRepository;
        _createUserPolicy = createUserPolicy;
        _passwordManager = passwordManager;
        _authManager = authManager;
    }

    public async Task<Result<UserDto, string>> CreateUserAsync(CreateUserCommand createUserCommand)
    {
        foreach (var policy in _createUserPolicy.Where(x => x.CanBeApplied(createUserCommand)))
            if (!await policy.IsValidAsync(createUserCommand))
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

    public async Task<Result<JsonWebToken, string>> SignInAsync(SingInCommand singInCommand)
    {
        var user = await _userRepository.GetByEmailAsync(singInCommand.Email);
        if (user is null || _passwordManager.Validate(singInCommand.Password, user.Password))
        {
            return Result<JsonWebToken, string>.Fail("Invalid Email or Password", StatusCodes.Status401Unauthorized);
        }

        return _authManager.CreateToken(user.Id, user.Email);
    }
}