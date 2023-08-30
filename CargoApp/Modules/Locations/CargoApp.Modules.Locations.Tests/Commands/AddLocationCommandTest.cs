﻿using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.Repositories;
using CargoApp.Modules.Locations.Core.Entities;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Locations.Tests.Commands;

public class AddLocationCommandTest
{
    private readonly IAddLocationCommandHandler _services;
    private IPolicy<AddLocationCommand> _policyMock = Substitute.For<IPolicy<AddLocationCommand>>();
    private ILocationRepository _locationRepositoryMock = Substitute.For<ILocationRepository>();

    public AddLocationCommandTest()
    {
        _services = new AddLocationCommandHandler(
            _locationRepositoryMock,
            new List<IPolicy<AddLocationCommand>> { _policyMock }
        );
    }

    [Fact]
    public async Task CreateLocation_WhenLocationExists_ThenReturnExistsLocationId()
    {
        // Arrange
        var locationId = Guid.NewGuid();
        
        _locationRepositoryMock.GetByOsmIdAsync(Arg.Any<long>()).Returns(new Location()
        {
            Id = locationId
        });

        // Act
        var result =
            await _services.Handle(new AddLocationCommand(new LocationDto(2.0, 2.0, "test", 100, "test",
                new AddressDto(null, null, null, null, null, null, null))));
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.SuccessModel.Should().Be(locationId.ToString());
    }

    [Fact]
    public async Task CreateLocation_WhenPolicyReturnError_ThenReturnError()
    {
        // Arrange
        _locationRepositoryMock.GetByOsmIdAsync(Arg.Any<long>()).Returns(null as Location);
        _policyMock.IsApplicable(Arg.Any<AddLocationCommand>()).Returns(true);
        _policyMock.IsValidAsync(Arg.Any<AddLocationCommand>()).Returns(false);
        _policyMock.ErrorMessage.Returns("error");
        
        // Act
        var result =
            await _services.Handle(new AddLocationCommand(new LocationDto(2.0, 2.0, "test", 100, "test",
                new AddressDto(null, null, null, null, null, null, null))));
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("error");

    }
    [Fact]
    
    public async Task CreateLocation_WhenPolicyReturnSuccess_ThenAddLocation()
    {
        // Arrange
        var sampleGuid = Guid.NewGuid();
        _locationRepositoryMock.GetByOsmIdAsync(Arg.Any<long>()).Returns(null as Location);
        _policyMock.IsApplicable(Arg.Any<AddLocationCommand>()).Returns(true);
        _policyMock.IsValidAsync(Arg.Any<AddLocationCommand>()).Returns(true);
        _locationRepositoryMock.CreateAsync(Arg.Any<Location>()).Returns(new Location()
        {
            Id = sampleGuid
        });
        
        // Act
        var result =
            await _services.Handle(new AddLocationCommand(new LocationDto(2.0, 2.0, "test", 100, "test",
                new AddressDto(null, null, null, null, null, null, null))));
        
        // Assert
        result.IsSuccess.Should().BeTrue();
        result.SuccessModel.Should().Be(sampleGuid.ToString());
        await _locationRepositoryMock.Received(1).CreateAsync(Arg.Any<Location>());
    }
}