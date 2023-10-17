namespace CargoApp.Modules.Cargoes.Core.DriverAggregate;

public interface IDriverRepository
{
    Task AddAsync(DriverAggregate.Driver driver);
}