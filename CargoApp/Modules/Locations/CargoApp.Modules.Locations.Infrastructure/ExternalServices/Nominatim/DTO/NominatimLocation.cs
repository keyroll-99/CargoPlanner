namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;

internal class NominatimLocation
{
    public required double Lat { get; init; }
    public required double Lon { get; init; }
    public required string Name { get; init; }
    public required string DisplayName { get; init; }
    public required long OsmId { get; init;}
    public required NominatimAddress Address { get; init; }
}