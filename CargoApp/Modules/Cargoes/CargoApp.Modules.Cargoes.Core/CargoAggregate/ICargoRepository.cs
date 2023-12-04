namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public interface ICargoRepository
{
    Task<Cargo> AddAsync(Cargo cargo);
}