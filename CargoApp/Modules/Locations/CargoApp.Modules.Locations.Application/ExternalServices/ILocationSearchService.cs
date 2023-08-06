using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;

namespace CargoApp.Modules.Locations.Application.ExternalServices;

public interface ILocationSearchService
{
    Task<Result<LocationDto>> Search(string query);
}