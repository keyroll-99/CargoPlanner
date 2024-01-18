namespace CargoApp.Modules.Contracts.Cargoes.Services;

public interface ICargoService
{
    Task<IList<CargoDto>> GetCargoesToPlan(Guid companyId);
}