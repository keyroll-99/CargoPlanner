using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocations;

internal class SearchLocationQueryHandler : IRequestHandler<SearchLocationQuery, ApiResult<IEnumerable<LocationDto>, string>>
{
    private readonly ILocationClientFactory _locationClientFactory;

    public SearchLocationQueryHandler(ILocationClientFactory locationClientFactory)
    {
        _locationClientFactory = locationClientFactory;
    }

    public async Task<ApiResult<IEnumerable<LocationDto>, string>> Handle(SearchLocationQuery query, CancellationToken cancellationToken)
    {
        return await _locationClientFactory.Create().Search(query.Query, new CancellationToken(false));
    }
}