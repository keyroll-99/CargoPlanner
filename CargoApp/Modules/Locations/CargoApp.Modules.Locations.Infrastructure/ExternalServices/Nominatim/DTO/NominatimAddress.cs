namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;

internal class NominatimAddress
{
    public string? City { get; init; }
    public string? CityDistrict { get; init; }
    public string? Continent { get; init; }
    public string? Country { get; init; }
    public string? CountryCode { get; init; }
    public string? HouseNumber { get; init; }
    public string? PostCode { get; init; }
}