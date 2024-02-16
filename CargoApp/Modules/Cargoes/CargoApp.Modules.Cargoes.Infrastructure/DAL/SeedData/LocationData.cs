using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;

namespace CargoApp.Modules.Cargoes.Infrastructure.DAL.SeedData;

internal class LocationData
{
    private static readonly IClock Clock = new Clock();

    public static readonly IList<Location> Locations = new List<Location>()
    {
        new Location(53.0198393,
            18.6366678,
            "Targowa, Jakubskie Przedmieście, Toruń",
            9376165895),
        new Location(53.1213699,
            18.002578754813108,
            "Bydgoszcz",
            139710904),
        new Location(52.2340504,
            20.9678918,
            "Warszawa",
            5521236633),
        new Location(54.51656415,
            18.54555707460149,
            "Gdynia", 
            49869951)
    };
}