using CargoApp.Core.Infrastructure.Repositories;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Locations.Application.Repositories;
using CargoApp.Modules.Locations.Core.Entities;
using CargoApp.Modules.Locations.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Locations.Infrastructure.Repositories;

public class LocationRepository : Repository<Location, LocationDbContext>, ILocationRepository
{
    public LocationRepository(LocationDbContext appContext, IClock clock) : base(appContext, clock)
    {
    }

    public Task<bool> ExistsByOsmIdAsync(long osmId)
        => Entities.AnyAsync(x => x.OsmId == osmId);

    public Task<Location?> GetByOsmIdAsync(long osmId)
        => Entities.FirstOrDefaultAsync(x => x.OsmId == osmId);
}