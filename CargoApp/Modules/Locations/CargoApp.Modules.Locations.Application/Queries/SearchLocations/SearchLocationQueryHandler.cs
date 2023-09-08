using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using MediatR;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocations;

internal class SearchLocationQueryHandler : IRequestHandler<SearchLocationQuery, Result<IEnumerable<LocationDto>, string>>
{
    private readonly ILocationClientFactory _locationClientFactory;

    public SearchLocationQueryHandler(ILocationClientFactory locationClientFactory)
    {
        _locationClientFactory = locationClientFactory;
    }

    public async Task<Result<IEnumerable<LocationDto>, string>> Handle(SearchLocationQuery query, CancellationToken cancellationToken)
    {
        return await _locationClientFactory.Create().Search(query.Query, new CancellationToken(false));
    }
}