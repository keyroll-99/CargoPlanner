using CargoApp.Modules.Locations.Application.DTO;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.ExternalServices.Locations;

internal interface ILocationClient
{
    Task<ApiResult<IEnumerable<LocationDto>>> Search(string query, CancellationToken cancellationToken);
}