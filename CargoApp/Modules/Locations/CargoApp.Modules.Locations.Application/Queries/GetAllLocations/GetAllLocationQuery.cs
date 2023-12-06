using CargoApp.Modules.Locations.Application.DTO;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.Queries.GetAllLocations;

public class GetAllLocationQuery : IRequest<ApiResult<IEnumerable<LocationDto>, string>>
{
    
}