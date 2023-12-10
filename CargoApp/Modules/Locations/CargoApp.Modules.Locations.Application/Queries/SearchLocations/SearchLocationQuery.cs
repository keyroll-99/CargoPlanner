using CargoApp.Modules.Locations.Application.DTO;
using MediatR;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocations;

public class SearchLocationQuery : IRequest<ApiResult<IEnumerable<LocationDto>, string>>
{
    public required string Query { get; init; }
}