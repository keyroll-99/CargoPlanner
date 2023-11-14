using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using CargoApp.Modules.Cargoes.Infrastructure.DAL;
using Microsoft.EntityFrameworkCore;

namespace CargoApp.Modules.Cargoes.Infrastructure.Repository;

internal class LocationRepository : ILocationRepository
{
    private readonly CargoDbContext _dbContext;

    public LocationRepository(CargoDbContext dbContext)
    {
        _dbContext = dbContext;
    }


    public Task<Location?> GetByOsmId(long osmId)
    {
        return _dbContext.Locations.AsNoTracking().SingleOrDefaultAsync(x => x.OsmId == osmId);
    }
}