using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using Result;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;

internal class CreateCargoDomainService : ICreateCargoDomainService
{
    private readonly IClock _clock;

    public CreateCargoDomainService(
        ILocationRepository locationRepository,
        ICompanyRepository companyRepository,
        ICargoRepository cargoRepository,
        IClock clock)
    {
        _clock = clock;
    }

    public Result<Cargo> CreateCargo(Location? from, Location? to, Company? sender, Company? receiver,
        DateTime expectedDeliveryTime) =>
        Cargo.Create(from, to, sender, receiver, expectedDeliveryTime, _clock);
}