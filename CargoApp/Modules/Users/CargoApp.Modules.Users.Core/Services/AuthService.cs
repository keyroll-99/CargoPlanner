﻿using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Security;
using Microsoft.AspNetCore.Http;
using SystemException = CargoApp.Core.Infrastructure.Exception.SystemException;

namespace CargoApp.Modules.Users.Core.Services;

public class AuthService : IAuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IEnumerable<IPolicy<CreateUserCommand>> _createUserPolicy;
    private readonly PasswordManager _passwordManager;

    public AuthService(
        IUserRepository userRepository, 
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicy,
        PasswordManager passwordManager)
    {
        _userRepository = userRepository;
        _createUserPolicy = createUserPolicy;
        _passwordManager = passwordManager;
    }

    public async Task<UserDto> CreateUserAsync(CreateUserCommand createUserCommand)
    {
        foreach (var policy in _createUserPolicy.Where(x => x.CanBeApplied(createUserCommand)))
        {
            if (!await policy.IsValid(createUserCommand))
            {
                // todo change this for custom exteption or match
                throw new SystemException(policy.ErrorMessage, StatusCodes.Status400BadRequest);
            }
        }

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