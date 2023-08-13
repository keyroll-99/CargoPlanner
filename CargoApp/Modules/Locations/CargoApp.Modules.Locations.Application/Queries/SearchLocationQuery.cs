using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Queries;

internal class SearchLocationQuery : ISearchLocationQuery
{
    private readonly ILocationClientFactory _locationClientFactory;

    public SearchLocationQuery(ILocationClientFactory locationClientFactory)
    {
        _locationClientFactory = locationClientFactory;
    }

    public async Task<IEnumerable<Location>> Search(string query)
    {
        var client = _locationClientFactory.Create();
        var result = await client.Search(query, new CancellationToken(false));
        if (result.IsSuccess)
        {
            return result.SuccessModel;
        }

        throw new Exception();
    }
}