using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Application.Queries.SearchLocations;
using NSubstitute;
using Result.ApiResult;
using Xunit;

namespace CargoApp.Modules.Locations.Tests.Queries;

public class SearchLocationQueryTest
{
    private readonly ILocationClientFactory _locationClientFactory = Substitute.For<ILocationClientFactory>();
    private readonly ILocationClient _locationClient = Substitute.For<ILocationClient>();
    private readonly SearchLocationQueryHandler _service;

    public SearchLocationQueryTest()
    {
        _locationClientFactory.Create().Returns(_locationClient);

        _service = new SearchLocationQueryHandler(_locationClientFactory);
    }

    [Fact]
    public async Task ShouldCallSearchMethod()
    {
        // Arrange
        var query = new SearchLocationQuery
        {
            Query = "QQuery"
        };
        _locationClient.Search(Arg.Any<string>(), Arg.Any<CancellationToken>())
            .Returns(ApiResult<IEnumerable<LocationDto>>.Success(new List<LocationDto>()));

        // Act
        var result = await _service.Handle(query, new CancellationToken(false));

        // Arrange 
        await _locationClient
            .Received(1)
            .Search(Arg.Is<string>(x => x == "QQuery"), Arg.Any<CancellationToken>());
    }
}