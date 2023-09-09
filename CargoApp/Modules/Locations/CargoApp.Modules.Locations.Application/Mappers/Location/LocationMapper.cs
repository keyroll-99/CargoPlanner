using System.Security.Cryptography.X509Certificates;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Mappers.Location;

internal static class LocationMapper
{
    public static LocationDto AsDto(this Core.Entities.Location location)
    {
        return new LocationDto(
            lat: location.Lat,
            lon: location.Lon,
            name: location.Name,
            displayName: location.DisplayName,
            osmId: location.OsmId,
            address: location.Address.AsDto()
        );
    }
    
    public static Core.Entities.Location AsEntity(this LocationDto location, Guid companyId)
    {
        return new Core.Entities.Location(
            lat: location.Lat,
            lon: location.Lon,
            name: location.Name,
            displayName: location.DisplayName,
            osmId: location.OsmId,
            companyId: companyId,
            address: location.Address.AsEntity()
        );
    }
}

file static class AddressMapper
{
    public static AddressDto AsDto(this Address address)
    {
        return new AddressDto(
            city: address.City,
            cityDistrict: address.CityDistrict,
            continent: address.Continent,
            country: address.Country,
            countryCode: address.CountryCode,
            houseNumber: address.HouseNumber,
            postCode: address.CountryCode);
    }
    
    public static Address AsEntity(this AddressDto address)
    {
        return new Address(
            city: address.City,
            cityDistrict: address.CityDistrict,
            continent: address.Continent,
            country: address.Country,
            countryCode: address.CountryCode,
            houseNumber: address.HouseNumber,
            postCode: address.CountryCode);
    }
}