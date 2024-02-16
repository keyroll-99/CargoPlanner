using CargoApp.Modules.Cargoes.Core.CargoAggregate;
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
        return _dbContext.Locations.SingleOrDefaultAsync(x => x.OsmId == osmId);
    }

    public async Task<Location> AddAsync(Location location)
    {
        var existingLocation = await _dbContext.Locations.AsNoTracking()
            .SingleOrDefaultAsync(x => x.OsmId == location.OsmId);

        if (existingLocation != null)
        {
            return existingLocation;
        }

        await _dbContext.Locations.AddAsync(location);
        await _dbContext.SaveChangesAsync();

        return location;
    }
}