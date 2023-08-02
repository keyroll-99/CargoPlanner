using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Clock;
using CargoApp.Core.Abstraction.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Policies;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services;
using CargoApp.Modules.Users.Core.Services.Abstract;
using CargoApp.Modules.Users.Core.Services.Impl;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Moq;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Unit.Services;

public class AuthServiceTests
{
    private readonly PasswordStrengthPolicy _samplePolicy = new();
    private readonly Mock<IUserRepository> _userRepository = new();
    private readonly Mock<IAuthManager> _authManager = new();
    private readonly Mock<IPasswordHasher<User>> _passwordHasher = new();
    private readonly Mock<IClock> _clock = new();
    private readonly IAuthService _service;

    public AuthServiceTests()
    {
        IEnumerable<IPolicy<CreateUserCommand>> createUserPolicies = new List<IPolicy<CreateUserCommand>>
        {
            _samplePolicy
        };
        _service = new AuthService(_userRepository.Object, createUserPolicies, _authManager.Object,
            _passwordHasher.Object, _clock.Object);
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

        _userRepository.Verify(x => x.CreateAsync(It.IsAny<User>()), Times.Once);
        _passwordHasher.Verify(
            x => x.HashPassword(It.IsAny<User>(), It.Is<string>(match => match == createUserCommand.Password)),
            Times.Once);
    }
}