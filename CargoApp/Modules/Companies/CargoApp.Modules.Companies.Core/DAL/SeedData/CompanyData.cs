using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Modules.Companies.Core.Entities;

namespace CargoApp.Modules.Companies.Core.DAL.SeedData;

internal class CompanyData
{
    private static readonly Clock Clock = new Clock();

    public static readonly Company Company = new Company(
        Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
        Clock.Now(),
        "Test",
        CompanyType.Ordering | CompanyType.Delivery);
}