namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public interface ILocationRepository
{
    public Task<Location?> GetByOsmId(long osmId);
    public Task<Location> AddAsync(Location location);
}