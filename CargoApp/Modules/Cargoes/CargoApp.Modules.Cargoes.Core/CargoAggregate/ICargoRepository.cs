namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public interface ICargoRepository
{
    Task<Cargo> AddAsync(Cargo cargo);
    Task<Cargo?> GetByIdAsync(Guid id);
    Task UpdateAsync(Cargo cargo);
}