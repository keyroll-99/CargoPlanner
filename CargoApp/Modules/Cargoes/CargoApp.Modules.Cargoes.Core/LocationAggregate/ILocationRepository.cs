namespace CargoApp.Modules.Cargoes.Core.LocationAggregate;

public interface ILocationRepository
{
    public Task<Location?> GetByOsmId(long osmId);
}