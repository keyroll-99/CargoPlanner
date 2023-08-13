using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using MediatR;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocation;

internal class SearchLocationQueryHandler : IRequestHandler<SearchLocationQuery, Result<IEnumerable<LocationDto>>>
{
    private readonly ILocationClientFactory _locationClientFactory;

    public SearchLocationQueryHandler(ILocationClientFactory locationClientFactory)
    {
        _locationClientFactory = locationClientFactory;
    }

    public async Task<Result<IEnumerable<LocationDto>>> Handle(SearchLocationQuery request, CancellationToken cancellationToken)
    {
        return await _locationClientFactory.Create().Search(request.Query, cancellationToken);
        
    }
}