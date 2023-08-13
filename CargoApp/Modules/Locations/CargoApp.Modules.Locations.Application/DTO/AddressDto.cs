namespace CargoApp.Modules.Locations.Application.DTO;

public class AddressDto
{
    public string? City { get; init; }
    public string? CityDistrict { get; init; }
    public string? Continent { get; init; }
    public string? Country { get; init; }
    public string? CountryCode { get; init; }
    public string? HouseNumber { get; init; }
    public string? PostCode { get; init; }

    public AddressDto(string? city, string? cityDistrict, string? continent, string? country, string? countryCode, string? houseNumber, string? postCode)
    {
        City = city;
        CityDistrict = cityDistrict;
        Continent = continent;
        Country = country;
        CountryCode = countryCode;
        HouseNumber = houseNumber;
        PostCode = postCode;
    }
}