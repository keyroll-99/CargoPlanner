using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Infrastructure.DAL.SeedData;

internal class LocationData
{
    private static readonly IClock Clock = new Clock();

    public static IList<Location> Locations = new[]
    {
        new Location(
            Guid.Parse("c535b351-5dc5-4f52-ae07-caa0c4fa1758"),
            Clock.Now(),
            53.0198393,
            18.6366678,
            "Targowa, Jakubskie Przedmieście, Toruń",
            "18B, Targowa, Jakubskie Przedmieście, Toruń, województwo kujawsko-pomorskie, 87-100, Polska",
            9376165895,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            new Address(
                "Targowa",
                "Jakubskie Przedmieście",
                null,
                "Polska",
                "pl",
                "18b",
                "87-100"
            )
        ),
        new Location(
            Guid.Parse("685b8426-796f-420e-a381-d7dee2152564"),
            Clock.Now(),
            53.1213699,
            18.002578754813108,
            "Bydgoszcz",
            "Miejska hala targowa, 7, Podwale, Stare Miasto, Śródmieście-Bocianowo-Stare Miasto, Bydgoszcz, województwo kujawsko-pomorskie, 85-111, Polska",
            139710904,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            new Address(
                "Bydgoszcz",
                "Śródmieście-Bocianowo-Stare Miasto",
                null,
                "Polska",
                "pl",
                "7",
                "85-111"
            )
        ),
        new Location(
            Guid.Parse("117c6892-d130-4395-bbdb-913d8a3c9c4d"),
            Clock.Now(),
            52.2340504,
            20.9678918,
            "Warszawa",
            "50A, Wolska, Kolonia Wawelberga, Młynów, Wola, Warszawa, województwo mazowieckie, 01-187, Polska",
            5521236633,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            new Address(
                "Warszawa",
                null,
                null,
                "Polska",
                "pl",
                "50",
                "01-187"
            )
        ),
        new Location(
            Guid.Parse("0c157af6-85d7-4760-b11b-c87f094be726"),
            Clock.Now(),
            54.51656415,
            18.54555707460149,
            "Gdynia",
            "Teatr Muzyczny im. Danuty Baduszkowej, 1, Plac Grunwaldzki, Śródmieście, Gdynia, województwo pomorskie, 81-372, Polska",
            49869951,
            Guid.Parse("d81e5308-93c6-4ba8-817d-f6b69e9ae703"),
            new Address(
                "Gdynia",
                null,
                null,
                "Polska",
                "pl",
                "1",
                "81-372"
            )
        )
    };
}