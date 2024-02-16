using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.SeedData;

public class DriverData
{
    private static readonly IClock Clock = new Clock();
    
    public static readonly IList<Driver> Drivers = new List<Driver>()
    {
        Driver.Create(
            LocationData.Locations[1],
            Guid.Parse("49ec2f72-be39-44ca-9f5d-d6816c305413")
        )!,
        Driver.Create(
            LocationData.Locations[1],
            Guid.Parse("3ee63160-4399-42d5-b99a-6d9f054aa53f")
        )!,
        Driver.Create(
            LocationData.Locations[2],
            Guid.Parse("d1e6b8a9-9b9e-4b0e-8b9a-9b9e4b0e8b9a")
        )!,
    };
    
}