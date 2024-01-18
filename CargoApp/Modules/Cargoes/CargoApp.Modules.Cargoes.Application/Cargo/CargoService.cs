using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using CargoApp.Modules.Contracts.Cargoes.Services;

namespace CargoApp.Modules.Cargoes.Application.Cargo;

public class CargoService : ICargoService
{
    private readonly ICargoRepository _cargoRepository;

    public CargoService(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    public async Task<IList<CargoDto>> GetCargoesToPlan(Guid companyId)
    {
         return (await _cargoRepository.GetAllToPlanForCompany(companyId)).Select(c => c.CreateDto()).ToList();
    }
}