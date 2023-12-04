using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate.DomainService;

public interface ICreateCargoDomainService
{
    public Cargo CreateCargo(Location? from, Location? to, Company? sender, Company? receiver, DateTime expectedDeliveryTime);
}