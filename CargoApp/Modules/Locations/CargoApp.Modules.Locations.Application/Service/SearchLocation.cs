using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Service;

internal class SearchLocation : ISearchLocation
{
    private ILocationClientFactory _locationClientFactory;

    public SearchLocation(ILocationClientFactory locationClientFactory)
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