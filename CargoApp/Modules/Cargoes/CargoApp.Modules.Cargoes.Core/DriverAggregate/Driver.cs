
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.DriverAggregate;

public class Driver
{
    public Guid Id { get; init; }
    private Location? _home;
    private Company _employer;
    private bool _isActive;

    private Driver(Location? home, Company employer, bool isActive, Guid id)
    {
        _home = home;
        _employer = employer;
        _isActive = isActive;
        Id = id;
    }

    public static Driver Create(Location home, Company employer)
    {
        return new Driver(home, employer, true, Guid.NewGuid());
    }
}