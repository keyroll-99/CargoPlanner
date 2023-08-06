using CargoApp.Modules.Locations.Core.Entities;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.Mappers;

internal static class NominatimLocationMapper
{
    public static Location AsLocation(this NominatimLocation location)
    {
        return new Location(
            lat: Convert.ToDouble(location.Lat),
            lon: Convert.ToDouble(location.Lon),
            name: location.Name,
            displayName: location.DisplayName,
            osmId: location.OsmId,
            address: location.Address.AsAddress()
        );
    }

    private static Address AsAddress(this NominatimAddress address)
    {
        return new Address(
            city: address.City,
            cityDistrict: address.CityDistrict,
            continent: address.Continent,
            country: address.Country,
            countryCode: address.CountryCode,
            houseNumber: address.HouseNumber,
            postCode: address.PostCode
        );
    }
}