

using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

public class Cargo
{
    public Guid Id { get; init; }
    private Location _from;
    private Location _to;
    private Company _sender;
    private Company _receiver;
    private DateTime _expectedDeliveryTime;
    private DateTime? _deliveryDate;
    private readonly DateTime _createAt;
    private DriverAggregate.Driver? _driver;
    
}