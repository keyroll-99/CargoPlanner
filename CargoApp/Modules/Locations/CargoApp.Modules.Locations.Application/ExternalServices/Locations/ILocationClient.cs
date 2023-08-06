using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.ExternalServices.Locations;

internal interface ILocationClient
{
    Task<Result<IEnumerable<Location>>> Search(string query, CancellationToken cancellationToken);
}