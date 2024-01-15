using CargoApp.Modules.Cargoes.Core.CompanyAggregate.ValueObject;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Contracts.Cargoes;

namespace CargoApp.Modules.Cargoes.Core.CompanyAggregate;

public class Company
{
    public Guid Id { get; init; }
    public Guid CompanyId { get; private set; }
    public CompanyName CompanyName { get; private set; }
    public ICollection<Driver> Drivers { get; private set; } = new List<Driver>();

    private Company(CompanyName companyName, Guid companyId)
    {
        CompanyName = companyName;
        CompanyId = companyId;
        Drivers = new List<Driver>();
    }

    public Company()
    {
    }

    public static Company Create(CompanyName name, Guid companyId)
    {
        return new Company(name, companyId)
        {
            Id = Guid.NewGuid()
        };
    }

    public void AddDriver(Driver driver)
    {
        Drivers.Add(driver);
    }

    public CompanyDto CreateDto()
    {
        return new CompanyDto(Id, CompanyId, CompanyName, Drivers.Select(x => x.CreateDto()).ToList());
    }
    
    public CompanyDto CreateDtoWithoutDrivers()
    {
        return new CompanyDto(Id, CompanyId, CompanyName, new List<DriverDto>());
    }
}