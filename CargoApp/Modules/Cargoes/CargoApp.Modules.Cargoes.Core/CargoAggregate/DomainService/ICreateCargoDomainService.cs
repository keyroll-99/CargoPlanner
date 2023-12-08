using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using Result;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;

public interface ICreateCargoDomainService
{
    public Result<Cargo> CreateCargo(Location? from, Location? to, Company? sender, Company? receiver, DateTime expectedDeliveryTime);
}