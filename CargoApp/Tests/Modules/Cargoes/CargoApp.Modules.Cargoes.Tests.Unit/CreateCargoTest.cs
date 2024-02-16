using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Cargoes.Tests.Unit;

public class CreateCargoTest
{
    private readonly IClock _clock = Substitute.For<IClock>();
    private readonly ILocationRepository _locationRepository = Substitute.For<ILocationRepository>();
    private readonly ICompanyRepository _companyRepository = Substitute.For<ICompanyRepository>();
    private readonly ICargoRepository _cargoRepository = Substitute.For<ICargoRepository>();

    private readonly CreateCargoCommandHandler _createCargoCommandHandler;

    public CreateCargoTest()
    {
        _createCargoCommandHandler = new CreateCargoCommandHandler(
            _companyRepository,
            _locationRepository,
            _cargoRepository,
            _clock
        );
    }

    [Fact]
    public async Task
        CreateCargoCommandHandler_ShouldReturnSuccessResult_WhenCommandIsCorrect()
    {
        // Arrange
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now
        );

        _locationRepository.GetByOsmId(Arg.Any<long>()).Returns(new Location(1, 1, "test1", 1));
        _companyRepository.GetByCompanyId(Arg.Any<Guid>()).Returns(new Company());
        _clock.Now().Returns(DateTime.Now.AddDays(-1));


        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.SuccessModel.Should().NotBeNull();
    }

    [Fact]
    public async Task CreateCargoCommandHandler_ShouldReturnFailResult_SourceIsNull()
    {
        // Arrange
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now
        );

        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Source can not be null");
    }

    [Fact]
    public async Task CreateCargoCommandHandler_ShouldReturnFailResult_DestinationIsNull()
    {
        // Arrange
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now
        );

        _locationRepository.GetByOsmId(Arg.Is<long>(x => x == 1)).Returns(new Location(1, 1, "test1", 1));


        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Destination can not be null");
    }

    [Fact]
    public async Task CreateCargoCommandHandler_ShouldReturnFailResult_SenderIsNull()
    {
        // Arrange
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now
        );

        _locationRepository.GetByOsmId(Arg.Any<long>()).Returns(new Location(1, 1, "test1", 1));


        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Sender can not be null");
    }

    [Fact]
    public async Task CreateCargoCommandHandler_ShouldReturnFailResult_ReceiverIsNull()
    {
        // Arrange
        var senderGuid = Guid.NewGuid();
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            senderGuid,
            Guid.NewGuid(),
            DateTime.Now
        );

        _locationRepository.GetByOsmId(Arg.Any<long>()).Returns(new Location(1, 1, "test1", 1));
        _companyRepository.GetByCompanyId(Arg.Is<Guid>(x => x == senderGuid)).Returns(new Company());

        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Receiver can not be null");
    }

    [Fact]
    public async Task CreateCargoCommandHandler_ShouldReturnFailResult_InvalidExpectedDeliveryTime()
    {
        // Arrange
        var senderGuid = Guid.NewGuid();
        _clock.Now().Returns(DateTime.Now);
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            senderGuid,
            Guid.NewGuid(),
            DateTime.Now.AddHours(-1)
        );

        _locationRepository.GetByOsmId(Arg.Any<long>()).Returns(new Location(1, 1, "test1", 1));
        _companyRepository.GetByCompanyId(Arg.Any<Guid>()).Returns(new Company());

        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("Can't create cargo with expected delivery time in past");
    }
}