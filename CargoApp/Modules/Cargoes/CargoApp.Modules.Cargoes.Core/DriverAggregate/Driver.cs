
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using Result;

namespace CargoApp.Modules.Cargoes.Core.DriverAggregate;

public class Driver
{
    public Guid Id { get; init; }
    private Guid _employeeId;
    private Location? _home;
    private Company _employer;
    private bool _isActive;

    private Driver(Location? home, Company employer, bool isActive, Guid id, Guid employeeId)
    {
        _home = home;
        _employer = employer;
        _isActive = isActive;
        Id = id;
        _employeeId = employeeId;
    }

    private Driver()
    {
    }

    public static Result<Driver> Create(Location? home, Company? employer, Guid employeeId)
    {
        if (employer is null)
        {
            return Result<Driver>.Fail("Employer cannot be null");
        }
        
        return new Driver(home, employer, true, Guid.Empty, employeeId);
    }
    
    public DriverDto CreateDto()
    {
        return new DriverDto(Id, _home?.CreateDto(), _employer.CreateDtoWithoutDrivers(), _isActive);
    }
}