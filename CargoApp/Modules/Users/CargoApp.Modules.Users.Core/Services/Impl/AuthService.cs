﻿using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Contracts.Companies;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Mappers;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Result.ApiResult;

namespace CargoApp.Modules.Users.Core.Services.Impl;

internal sealed class AuthService : IAuthService
{
    private readonly IEnumerable<IPolicy<CreateUserCommand>> _createUserPolicy;
    private readonly IUserRepository _userRepository;
    private readonly IAuthManager _authManager;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly ICompany _companyServices;

    public AuthService(
        IUserRepository userRepository,
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicy,
        IAuthManager authManager,
        IPasswordHasher<User> passwordHasher,
        ICompany companyServices)
    {
        _userRepository = userRepository;
        _createUserPolicy = createUserPolicy;
        _authManager = authManager;
        _passwordHasher = passwordHasher;
        _companyServices = companyServices;
    }

    public async Task<ApiResult<UserDto>> CreateUserAsync(CreateUserCommand createUserCommand)
    {
        foreach (var policy in _createUserPolicy.Where(x => x.IsApplicable(createUserCommand)))
            if (!await policy.IsValidAsync(createUserCommand))
                return policy.ErrorMessage;

        var model = new User
        {
            Email = createUserCommand.Email,
            Id = Guid.NewGuid(),
            Password = _passwordHasher.HashPassword(default, createUserCommand.Password),
            IsActive = true
        };
        await _userRepository.AddAsync(model);

        return ApiResult<UserDto>.Success(model.AsUserDto(), StatusCodes.Status201Created);
    }

    public async Task<ApiResult<JsonWebToken>> SignInAsync(SingInCommand singInCommand)
    {
        var user = await _userRepository.GetByEmailAsync(singInCommand.Email);
        if (user is null || _passwordHasher.VerifyHashedPassword(default, user.Password, singInCommand.Password) ==
            PasswordVerificationResult.Failed || !user.IsActive)
        {
            return ApiResult<JsonWebToken>.Fail("Invalid Email or Password", StatusCodes.Status401Unauthorized);
        }

        var company = user.EmployeeId.HasValue
            ? await _companyServices.FindEmployeeCompany(user.EmployeeId.Value)
            : null;

        var token = _authManager.CreateToken(user.Id, user.Email, user.PermissionMask, company?.Id ?? Guid.Empty);
        return token;
    }
}