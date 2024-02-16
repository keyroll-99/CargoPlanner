namespace CargoApp.Modules.Contracts.Cargoes.Services;

public interface IDriverService
{
    public Task<IEnumerable<CargoDto>> GetPlannedDriverCargoes(Guid driverId);
}