using System.Text.Json.Serialization;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;

internal class NominatimAddress
{
    public string? City { get; init; }
    [JsonPropertyName("city_district")]
    public string? CityDistrict { get; init; }
    [JsonPropertyName("continent")]
    public string? Continent { get; init; }
    public string? Country { get; init; }
    [JsonPropertyName("country_code")]
    public string? CountryCode { get; init; }
    [JsonPropertyName("house_number")]
    public string? HouseNumber { get; init; }
    [JsonPropertyName("postcode")]
    public string? PostCode { get; init; }
}