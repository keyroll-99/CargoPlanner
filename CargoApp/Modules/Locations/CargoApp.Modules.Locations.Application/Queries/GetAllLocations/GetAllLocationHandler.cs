using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.Mappers.Location;
using CargoApp.Modules.Locations.Application.Repositories;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.Queries.GetAllLocations;

public class GetAllLocationHandler : IRequestHandler<GetAllLocationQuery, ApiResult<IEnumerable<LocationDto>, string>>
{
    private readonly ILocationRepository _locationRepository;

    public GetAllLocationHandler(ILocationRepository locationRepository)
    {
        _locationRepository = locationRepository;
    }

    public async Task<ApiResult<IEnumerable<LocationDto>, string>> Handle(GetAllLocationQuery request, CancellationToken cancellationToken)
    {
        var result = await _locationRepository.GetAllAsync();
        return ApiResult<IEnumerable<LocationDto>, string>.Success(result.Select(x => x.AsDto()));
    }
}