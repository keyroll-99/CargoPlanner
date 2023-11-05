
using CargoApp.Core.Infrastructure.Response;
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

    private Driver()
    {
    }

    public static Result<Driver> Create(Location? home, Company? employer)
    {
        if (employer is null)
        {
            return Result<Driver>.Fail("Employer cannot be null");
        }
        
        return new Driver(home, employer, true, Guid.Empty);
    }
}