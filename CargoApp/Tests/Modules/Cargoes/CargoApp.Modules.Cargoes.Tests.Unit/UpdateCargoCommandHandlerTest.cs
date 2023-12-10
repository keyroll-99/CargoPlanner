using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Application.Cargo.UpdateCargo;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Cargoes.Tests.Unit;

public class UpdateCargoCommandHandlerTest
{
    private readonly ICargoRepository _cargoRepository = Substitute.For<ICargoRepository>();
    private readonly IClock _clock = Substitute.For<IClock>();
    private readonly ILocationRepository _locationRepository = Substitute.For<ILocationRepository>();
    private readonly ICompanyRepository _companyRepository = Substitute.For<ICompanyRepository>();

    private readonly UpdateCargoCommandHandler _cargoCommandHandler;

    public UpdateCargoCommandHandlerTest()
    {
        _cargoCommandHandler = new UpdateCargoCommandHandler(
            _cargoRepository,
            _clock,
            _locationRepository,
            _companyRepository
        );
    }
    
    [Fact]
    public async Task UpdateCargoCommandHandler_ShouldReturnFailResult_WhenCargoNotFound()
    {
        // Arrange
        var updateCargoCommand = new UpdateCargoCommand(
            Guid.NewGuid(),
            null,
            null,
            null,
            null
        );

        // Act
        var result = await _cargoCommandHandler.Handle(updateCargoCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Cargo not found");
    }
    
    [Fact]
    public async Task UpdateCargoCommandHandler_ShouldReturnFailResult_WhenSourceNotFound()
    {
        // Arrange
        var updateCargoCommand = new UpdateCargoCommand(
            Guid.NewGuid(),
            1,
            null,
            null,
            null
        );

        _cargoRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Cargo());
        
        // Act
        var result = await _cargoCommandHandler.Handle(updateCargoCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Source not found");
    }
    
    [Fact]
    public async Task UpdateCargoCommandHandler_ShouldReturnFailResult_WhenDestinationNotFound()
    {
        // Arrange
        var updateCargoCommand = new UpdateCargoCommand(
            Guid.NewGuid(),
            null,
            1,
            null,
            null
        );

        _cargoRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Cargo());
        
        // Act
        var result = await _cargoCommandHandler.Handle(updateCargoCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Destination not found");
    }
    
    [Fact]
    public async Task UpdateCargoCommandHandler_ShouldReturnFailResult_WhenReceiverNotFound()
    {
        // Arrange
        var updateCargoCommand = new UpdateCargoCommand(
            Guid.NewGuid(),
            null,
            null,
            Guid.NewGuid(),
            null
        );

        _cargoRepository.GetByIdAsync(Arg.Any<Guid>()).Returns(new Cargo());
        _locationRepository.GetByOsmId(Arg.Any<long>()).Returns(new Location(2, 2, "test", 1));
        
        // Act
        var result = await _cargoCommandHandler.Handle(updateCargoCommand, CancellationToken.None);
        
        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Receiver not found");
    }
}