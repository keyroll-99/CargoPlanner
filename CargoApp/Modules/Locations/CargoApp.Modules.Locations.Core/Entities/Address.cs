using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Locations.Core.Entities;

public class Address : BaseEntity
{
    public string? City { get; private set; }
    public string? CityDistrict { get; private set; }
    public string? Continent { get; private set; }
    public string? Country { get; private set; }
    public string? CountryCode { get; private set; }
    public string? HouseNumber { get; private set; }
    public string? PostCode { get; private set; }

    public Address(string? city, string? cityDistrict, string? continent, string? country, string? countryCode, string? houseNumber, string? postCode)
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