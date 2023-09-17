using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Module.Contracts.Companies;
using CargoApp.Modules.Contracts.Companies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Policies;
using CargoApp.Modules.Users.Core.Policies.CreateUserCommandPolicies;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using CargoApp.Modules.Users.Core.Services.Impl;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Unit.Services;

public class AuthServiceTests
{
    private readonly PasswordStrengthPolicy _samplePolicy = new();
    private readonly IUserRepository _userRepository = Substitute.For<IUserRepository>();
    private readonly IAuthManager _authManager = Substitute.For<IAuthManager>();
    private readonly IPasswordHasher<User> _passwordHasher = Substitute.For<IPasswordHasher<User>>();
    private readonly IClock _clock = Substitute.For<IClock>();
    private readonly ICompany _company = Substitute.For<ICompany>();
    private readonly IAuthService _service;

    public AuthServiceTests()
    {
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicies = new List<IPolicy<CreateUserCommand>>
        {
            _samplePolicy
        };
        _service = new AuthService(_userRepository, createUserPolicies, _authManager,
            _passwordHasher, _clock, _company);
    }

    [Fact]
    public async Task CreateUser_WhenOnePolicyReturnError_ThenReturnError()
    {
        // act
        var result = await _service.CreateUserAsync(new CreateUserCommand("aaa", "aaa"));

        // assert
        result.IsSuccess.Should().BeFalse();
        result.SuccessModel.Should().BeNull();
        result.ErrorModel.Should().Be(_samplePolicy.ErrorMessage);
    }

    [Fact]
    public async Task CreateUser_WhenCommandIsCorrect_ThenUserModel()
    {
        // arrange
        var createUserCommand = new CreateUserCommand("aaa", "aaaaaaaaaaa");

        // act
        var result = await _service.CreateUserAsync(createUserCommand);

        // assert
        result.IsSuccess.Should().BeTrue();
        result.SuccessModel.Should().NotBeNull();
        result.ErrorModel.Should().BeNull();
        result.SuccessModel.Email.Should().Be(createUserCommand.Email);
        result.SuccessModel.IsActive.Should().BeTrue();

        _userRepository.Received(1).AddAsync(Arg.Any<User>());
        _passwordHasher.Received(1)
            .HashPassword(Arg.Any<User>(), Arg.Is<string>(match => match == createUserCommand.Password));
    }
}