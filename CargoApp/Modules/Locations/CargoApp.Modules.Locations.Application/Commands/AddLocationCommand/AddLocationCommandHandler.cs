using CargoApp.Core.Abstraction.Repositories;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Locations.Application.Mappers.Location;
using CargoApp.Modules.Locations.Application.Repositories;
using CargoApp.Modules.Locations.Core.Entities;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;

public class AddLocationCommandHandler : IAddLocationCommandHandler
{
    private readonly ILocationRepository _locationRepository;
    private readonly IEnumerable<IPolicy<AddLocationCommand>> _policies;
    
    public AddLocationCommandHandler(ILocationRepository locationRepository, IEnumerable<IPolicy<AddLocationCommand>> policies)
    {
        _locationRepository = locationRepository;
        _policies = policies;
    }

    public async Task<Result<string, string>> Handle(AddLocationCommand command)
    {
        var commandLocation = command.Location;
        var existsLocation = await _locationRepository.GetByOsmIdAsync(commandLocation.OsmId);
        if (existsLocation is not null)
        {
            return Result<string, string>.Success(existsLocation.Id.ToString());
        }

        foreach (var policy in _policies.Where(x => x.CanBeApplied(command)))
        {
            if (!await policy.IsValidAsync(command))
            {
                return Result<string, string>.Fail(policy.ErrorMessage, policy.StatusCode);
            }
        }

        var location = command.Location.AsEntity();
        var result = await _locationRepository.CreateAsync(location);
        return Result<string, string>.Success(result.Id.ToString(), StatusCodes.Status201Created);
    }
}
