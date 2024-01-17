using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.SeedData;

public static class CompanyData
{
    private static readonly IClock Clock = new Clock();
    
    public static readonly List<Company> Companies = new()
    {
        Company.Create("test", Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"))
    };
}