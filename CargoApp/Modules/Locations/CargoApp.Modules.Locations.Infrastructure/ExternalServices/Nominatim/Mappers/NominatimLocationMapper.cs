using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Core.Entities;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.Mappers;

internal static class NominatimLocationMapper
{
    public static LocationDto AsLocationDto(this NominatimLocation location)
    {
        return new LocationDto(
            lat: Convert.ToDouble(location.Lat),
            lon: Convert.ToDouble(location.Lon),
            name: location.DisplayName.Split(",").FirstOrDefault() ?? "",
            displayName: location.DisplayName,
            osmId: location.OsmId,
            address: location.Address.AsAddressDto()
        );
    }

    private static AddressDto AsAddressDto(this NominatimAddress address)
    {
        return new AddressDto(
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