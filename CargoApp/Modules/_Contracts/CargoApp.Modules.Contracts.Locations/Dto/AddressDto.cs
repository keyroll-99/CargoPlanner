namespace CargoApp.Modules.Locations.Application.DTO;

public record AddressDto
{
    public string? City { get; private set; }
    public string? CityDistrict { get; private set; }
    public string? Continent { get; private set; }
    public string? Country { get; private set; }
    public string? CountryCode { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? PostCode { get; private set; }

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