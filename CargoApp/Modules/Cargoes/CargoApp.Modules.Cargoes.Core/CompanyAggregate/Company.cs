using CargoApp.Modules.Cargoes.Core.CompanyAggregate.ValueObject;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;

namespace CargoApp.Modules.Cargoes.Core.CompanyAggregate;

public class Company
{
    public Guid Id { get; init; }
    private Guid _companyId;
    private CompanyName _companyName;
    private IList<Driver> _drivers;

    private Company(CompanyName companyName, Guid companyId)
    {
        _companyName = companyName;
        _companyId = companyId;
        _drivers = new List<Driver>();
    }

    public static Company Create(CompanyName name, Guid companyId)
    {
        return new Company(name, companyId)
        {
            Id = Guid.NewGuid()
        };
    }
}