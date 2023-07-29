using System.Security.Cryptography;
using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Clock;
using CargoApp.Core.Abstraction.Policies;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CargoApp.Modules.Users.Core.Services;

internal class AuthService : IAuthService
{
    private readonly IEnumerable<IPolicy<CreateUserCommand>> _createUserPolicy;
    private readonly IUserRepository _userRepository;
    private readonly IClock _clock;
    private readonly IAuthManager _authManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;

    public AuthService(
        IUserRepository userRepository,
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicy,
        IAuthManager authManager,
        IPasswordHasher<User> passwordHasher, 
        IRefreshTokenRepository refreshTokenRepository,
        IClock clock)
    {
        _userRepository = userRepository;
        _createUserPolicy = createUserPolicy;
        _authManager = authManager;
        _passwordHasher = passwordHasher;
        _refreshTokenRepository = refreshTokenRepository;
        _clock = clock;
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
            Password = _passwordHasher.HashPassword(default, createUserCommand.Password),
            IsActive = true
        };
        await _userRepository.CreateAsync(model);

        return new UserDto(model.Id, model.Email, model.IsActive);
    }

    public async Task<Result<JsonWebToken, string>> SignInAsync(SingInCommand singInCommand)
    {
        var user = await _userRepository.GetByEmailAsync(singInCommand.Email);
        if (user is null || _passwordHasher.VerifyHashedPassword(default, user.Password, singInCommand.Password) ==
            PasswordVerificationResult.Failed)
        {
            return Result<JsonWebToken, string>.Fail("Invalid Email or Password", StatusCodes.Status401Unauthorized);
        }

        var token = _authManager.CreateToken(user.Id, user.Email);
        token.RefreshToken = await GenerateRefreshToken(user);
        return token;
    }

    
    // todo move it to other service
    private async Task<string> GenerateRefreshToken(User user)
    {
        while (true)
        {
            var token = Convert.ToBase64String(RandomNumberGenerator.GetBytes(64));
            var existToken = await _refreshTokenRepository.TokenExistsAsync(token);

            if (existToken)
            {
                continue;
            }

            return token;
        }
    }
}