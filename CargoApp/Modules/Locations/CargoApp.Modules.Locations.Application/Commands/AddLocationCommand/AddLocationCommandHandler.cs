using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.Repositories;
using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public class AddLocationCommandHandler : IAddLocationCommandHandler
{
    private IRepositoryFactory _repositoryFactory;
    private ILocationRepository _locationRepository;
    
    public AddLocationCommandHandler(IRepositoryFactory repositoryFactory)
    {
        _repositoryFactory = repositoryFactory;
        _locationRepository = repositoryFactory.GetRepository<ILocationRepository>();
    }

    public async Task<Result<string, string>> Handle(AddLocationCommand command)
    {
        // _locationRepository = _repositoryFactory.GetRepository<ILocationRepository>();
        var commandLocation = command.Location;
        var existsLocation = await _locationRepository.GetByOsmIdAsync(commandLocation.OsmId);
        if (existsLocation is not null)
        {
            return Result<string, string>.Success(existsLocation.Id.ToString());
        }
        
        var location = new Location(
            lat: commandLocation.Lat,
            lon: commandLocation.Lon,
            name: commandLocation.Name,
            displayName: commandLocation.DisplayName,
            osmId: commandLocation.OsmId,
            address: new Address(
                city: commandLocation.Address.City,
                cityDistrict: commandLocation.Address.CityDistrict,
                continent: commandLocation.Address.Continent,
                country: commandLocation.Address.Country,
                countryCode: commandLocation.Address.CountryCode,
                houseNumber: commandLocation.Address.HouseNumber,
                postCode: commandLocation.Address.CountryCode
            )
        );

        var result = await _locationRepository.CreateAsync(location);
        return Result<string, string>.Success(result.Id.ToString());
    }
}
