using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using CargoApp.Modules.Contracts.Cargoes.Services;

namespace CargoApp.Modules.Cargoes.Application.Driver;

public class DriverService : IDriverService
{
    private readonly ICargoRepository _cargoRepository;

    public DriverService(ICargoRepository cargoRepository)
    {
        _cargoRepository = cargoRepository;
    }

    public async Task<IEnumerable<CargoDto>> GetPlannedDriverCargoes(Guid driverId)
    {
        return (await _cargoRepository.GetAllPlannedForDriver(driverId)).Select(c => c.CreateDto());
    }
}