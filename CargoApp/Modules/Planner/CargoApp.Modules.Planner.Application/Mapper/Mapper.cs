using CargoApp.Modules.Contracts.Cargoes;
using CargoApp.Modules.Planner.Core.Planner;

namespace CargoApp.Modules.Planner.Application.Mapper;

internal static class Mapper
{
    public static Driver AsDriver(this DriverDto driverDto)
    {
        return new Driver
        {
            Home = driverDto.Home.AsLocation(),
            Id = driverDto.Id
        };
    }
    
    public static Cargo AsCargo(this CargoDto cargoDto)
    {
        return new Cargo
        {
            Id = cargoDto.Id,
            From = cargoDto.From.AsLocation(),
            To = cargoDto.To.AsLocation(),
            ExpectedDeliveryTime = cargoDto.ExpectedDeliveryTime,
            Driver = cargoDto.Driver?.AsDriver(),
        };
    }
    
    public static Company AsCompany(this CompanyDto companyDto)
    {
        return new Company
        {
            Id = companyDto.Id,
            Name = companyDto.CompanyName,
            Drivers = companyDto.Drivers.Select(d => d.AsDriver()).ToList()
        };
    }

    private static Location AsLocation(this LocationDto locationDto)
    {
        return new Location
        {
            Lat = locationDto.Lat,
            Lon = locationDto.Lon,
            Name = locationDto.Name,
            OsmId = locationDto.OsmId
        };
    }
}