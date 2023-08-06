using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices;

public class NominatimService : ILocationSearchService
{
    // TODO inject request
    public Task<Result<LocationDto>> Search(string query)
    {
        throw new NotImplementedException();
    }
}