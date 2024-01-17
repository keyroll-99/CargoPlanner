using CargoApp.Modules.Cargoes.Core.LocationAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using Result;

namespace CargoApp.Modules.Cargoes.Core.CompanyAggregate;

public class Driver
{
    public Guid Id { get; init; }
    public Guid EmployeeId { get; private set; }
    public Location? Home{ get; private set; }
    public bool IsActive{ get; private set; }

    private Driver(Location? home, bool isActive, Guid id, Guid employeeId)
    {
        Home = home;
        IsActive = isActive;
        Id = id;
        EmployeeId = employeeId;
    }

    private Driver()
    {
    }

    public static Result<Driver> Create(Location? home, Guid employeeId)
    {
        
        return new Driver(home, true, Guid.Empty, employeeId);
    }
    
    public DriverDto CreateDto()
    {
        return new DriverDto(Id, Home?.CreateDto(), IsActive);
    }
}