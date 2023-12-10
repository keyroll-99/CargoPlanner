namespace CargoApp.Modules.Cargoes.Core.LocationAggregate;

public interface ILocationRepository
{
    public Task<Location?> GetByOsmId(long osmId);
    public Task<Location> AddAsync(Location location);
}