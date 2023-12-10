using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Cargoes.Tests.Unit;

public class CreateCargoTest
{
    private readonly ICreateCargoDomainService _createCargoDomainService = Substitute.For<ICreateCargoDomainService>();
    private readonly ILocationRepository _locationRepository = Substitute.For<ILocationRepository>();
    private readonly ICompanyRepository _companyRepository = Substitute.For<ICompanyRepository>();
    private readonly ICargoRepository _cargoRepository = Substitute.For<ICargoRepository>();

    private readonly CreateCargoCommandHandler _createCargoCommandHandler;

    public CreateCargoTest()
    {
        _createCargoCommandHandler = new CreateCargoCommandHandler(
            _createCargoDomainService,
            _companyRepository,
            _locationRepository,
            _cargoRepository
        );
    }

    [Fact]
    public async Task
        CreateCargoCommandHandler_ShouldReturnSuccessResult_WhenCreateCargoDomainServiceReturnsSuccessResult()
    {
        // Arrange
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now
        );

        var createCargoResult = Cargo.Create(
            new Location(1, 1, "test1", 1),
            new Location(2, 2, "test2", 2),
            new Company(),
            new Company(),
            DateTime.Now,
            new Clock());


        _createCargoDomainService.CreateCargo(
            Arg.Any<Location>(),
            Arg.Any<Location>(),
            Arg.Any<Company>(),
            Arg.Any<Company>(),
            Arg.Any<DateTime>()
        ).Returns(createCargoResult);

        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeTrue();
        result.SuccessModel.Should().NotBeNull();
    }
    
    [Fact]
    public async Task CreateCargoCommandHandler_ShouldReturnFailResult_WhenCreateCargoDomainServiceReturnsFailResult()
    {
        // Arrange
        var createCargoCommand = new CreateCargoCommand(
            1,
            2,
            Guid.NewGuid(),
            Guid.NewGuid(),
            DateTime.Now
        );

        var createCargoResult = Result.Result<Cargo>.Fail("test");

        _createCargoDomainService.CreateCargo(
            Arg.Any<Location>(),
            Arg.Any<Location>(),
            Arg.Any<Company>(),
            Arg.Any<Company>(),
            Arg.Any<DateTime>()
        ).Returns(createCargoResult);

        // Act
        var result = await _createCargoCommandHandler.Handle(createCargoCommand, CancellationToken.None);

        // Assert
        result.IsSuccess.Should().BeFalse();
        result.ErrorModel.Should().Be("test");
    }
}