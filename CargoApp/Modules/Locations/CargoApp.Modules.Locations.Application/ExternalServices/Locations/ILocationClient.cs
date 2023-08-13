using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.ExternalServices.Locations;

internal interface ILocationClient
{
    Task<Result<IEnumerable<LocationDto>>> Search(string query, CancellationToken cancellationToken);
}