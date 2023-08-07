using System.Text.Json.Serialization;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;

internal class NominatimLocation
{
    public required string Lat { get; init; }
    public required string Lon { get; init; }
    [JsonPropertyName("display_name")]
    public required string DisplayName { get; init; }
    [JsonPropertyName("osm_id")]
    public required long OsmId { get; init;}
    public required NominatimAddress Address { get; init; }
}