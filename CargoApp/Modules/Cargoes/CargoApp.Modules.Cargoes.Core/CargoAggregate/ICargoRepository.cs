namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public interface ICargoRepository
{
    Task<Cargo> AddAsync(Cargo cargo);
    Task<Cargo?> GetByIdAsync(Guid id);
    Task<List<Cargo>> GetPageAsync(int page, int pageSize);
    Task<List<Cargo>> GetAllToPlanForCompany(Guid companyId);
    Task<List<Cargo>> GetAllPlannedForDriver(Guid driverId);
    Task UpdateAsync(Cargo cargo);
}