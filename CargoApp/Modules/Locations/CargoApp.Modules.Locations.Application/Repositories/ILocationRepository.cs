using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Repositories;

public interface ILocationRepository : IRepository<Location, Guid>
{
    Task<bool> ExistsByOsmIdAsync(long osmId);
    Task<Location?> GetByOsmIdAsync(long osmId);
}