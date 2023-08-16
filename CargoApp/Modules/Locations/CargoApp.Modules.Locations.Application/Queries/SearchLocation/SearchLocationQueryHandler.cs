using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocation;

internal class SearchLocationQueryHandler : ISearchLocationQueryHandler
{
    private readonly ILocationClientFactory _locationClientFactory;

    public SearchLocationQueryHandler(ILocationClientFactory locationClientFactory)
    {
        _locationClientFactory = locationClientFactory;
    }

    public async Task<Result<IEnumerable<LocationDto>, string>> Handle(SearchLocationQuery query)
    {
        return await _locationClientFactory.Create().Search(query.Query, new CancellationToken(false));
    }
}