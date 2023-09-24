using CargoApp.Core.ShareCore.Enums;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using CargoApp.Modules.Users.Core.Repositories;
using CargoApp.Modules.Users.Core.Services.Abstract;
using CargoApp.Modules.Users.Core.Services.Impl;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Unit.Services;

public class PermissionToolsTest
{
    private readonly IUserRepository _userRepositoryMock = Substitute.For<IUserRepository>();
    private readonly IPolicy<UpdatePermissionCommand> _policyMock = Substitute.For<IPolicy<UpdatePermissionCommand>>();

    private readonly IPermissionTools _service;

    public PermissionToolsTest()
    {
        var policiesMock = new List<IPolicy<UpdatePermissionCommand>>
        {
            _policyMock
        };

        _service = new PermissionTools(
            policiesMock,
            _userRepositoryMock
        );
    }

    [Fact]
    public async Task AddPermission_WhenUserDoesntExist_ThenReturnError()
    {
        // arrange
        _userRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(null as User);

        // act
        var result = await _service.AddPermission(new UpdatePermissionCommand(PermissionEnum.Cargoes, Guid.NewGuid()));

        // assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Should().Be("User doesn't exists");
    }

    [Fact]
    public async Task AddPermission_WhenPolicyReturnFalse_ThenReturnError()
    {
        // arrange
        _userRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(new User
        {
            Email = "elo",
            Password = "pass"
        });

        _policyMock.IsApplicable(Arg.Any<UpdatePermissionCommand>()).Returns(true);
        _policyMock.IsValidAsync(Arg.Any<UpdatePermissionCommand>()).Returns(false);
        _policyMock.ErrorMessage.Returns("Error");
        
        // act
        var result = await _service.AddPermission(new UpdatePermissionCommand(PermissionEnum.Cargoes, Guid.NewGuid()));

        // assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Should().Be("Error");
    }
    
    [Fact]
    public async Task AddPermission_WhenPolicyReturnTrue_ThenAddPermissionToUser()
    {
        // arrange
        _userRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(new User
        {
            Email = "elo",
            Password = "pass",
            PermissionMask = 0
        });

        _policyMock.IsApplicable(Arg.Any<UpdatePermissionCommand>()).Returns(true);
        _policyMock.IsValidAsync(Arg.Any<UpdatePermissionCommand>()).Returns(true);
        _policyMock.ErrorMessage.Returns("Error");
        
        // act
        var result = await _service.AddPermission(new UpdatePermissionCommand(PermissionEnum.Cargoes, Guid.NewGuid()));

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        await _userRepositoryMock.Received(1).UpdateAsync(Arg.Is<User>(x => x.PermissionMask == PermissionEnum.Cargoes));
    }
    
    [Fact]
    public async Task RemovePermission_WhenUserDoesntExist_ThenReturnError()
    {
        // arrange
        _userRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(null as User);

        // act
        var result = await _service.RemovePermission(new UpdatePermissionCommand(PermissionEnum.Cargoes, Guid.NewGuid()));

        // assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Should().Be("User doesn't exists");
    }

    [Fact]
    public async Task RemovePermission_WhenPolicyReturnFalse_ThenReturnError()
    {
        // arrange
        _userRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(new User
        {
            Email = "elo",
            Password = "pass"
        });

        _policyMock.IsApplicable(Arg.Any<UpdatePermissionCommand>()).Returns(true);
        _policyMock.IsValidAsync(Arg.Any<UpdatePermissionCommand>()).Returns(false);
        _policyMock.ErrorMessage.Returns("Error");
        
        // act
        var result = await _service.RemovePermission(new UpdatePermissionCommand(PermissionEnum.Cargoes, Guid.NewGuid()));

        // assert
        result.IsSuccess.Should().BeFalse();
        result.Error.Should().NotBeNull();
        result.Error.Should().Be("Error");
    }
    
    [Fact]
    public async Task RemovePermission_WhenPolicyReturnTrue_ThenAddPermissionToUser()
    {
        // arrange
        _userRepositoryMock.GetByIdAsync(Arg.Any<Guid>()).Returns(new User
        {
            Email = "elo",
            Password = "pass",
            PermissionMask = PermissionEnum.Cargoes
        });

        _policyMock.IsApplicable(Arg.Any<UpdatePermissionCommand>()).Returns(true);
        _policyMock.IsValidAsync(Arg.Any<UpdatePermissionCommand>()).Returns(true);
        _policyMock.ErrorMessage.Returns("Error");
        
        // act
        var result = await _service.RemovePermission(new UpdatePermissionCommand(PermissionEnum.Cargoes, Guid.NewGuid()));

        // assert
        result.IsSuccess.Should().BeTrue();
        result.Error.Should().BeNull();
        await _userRepositoryMock.Received(1).UpdateAsync(Arg.Is<User>(x => x.PermissionMask == 0));
    }
    
}