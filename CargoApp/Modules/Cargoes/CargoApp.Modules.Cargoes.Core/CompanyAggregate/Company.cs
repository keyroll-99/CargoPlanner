using CargoApp.Modules.Cargoes.Core.CompanyAggregate.ValueObject;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Contracts.Cargoes;

namespace CargoApp.Modules.Cargoes.Core.CompanyAggregate;

public class Company
{
    public Guid Id { get; init; }
    public Guid CompanyId { get; private set; }
    private CompanyName _companyName;
    private ICollection<Driver> _drivers = new List<Driver>();

    private Company(CompanyName companyName, Guid companyId)
    {
        _companyName = companyName;
        CompanyId = companyId;
        _drivers = new List<Driver>();
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
        _drivers.Add(driver);
    }

    public CompanyDto CreateDto()
    {
        return new CompanyDto(Id, CompanyId, _companyName, _drivers.Select(x => x.CreateDto()).ToList());
    }
    
    public CompanyDto CreateDtoWithoutDrivers()
    {
        return new CompanyDto(Id, CompanyId, _companyName, new List<DriverDto>());
    }
}