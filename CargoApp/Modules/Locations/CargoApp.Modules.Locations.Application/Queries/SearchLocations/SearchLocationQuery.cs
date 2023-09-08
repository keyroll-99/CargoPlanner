using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using MediatR;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocations;

public class SearchLocationQuery : IRequest<Result<IEnumerable<LocationDto>, string>>
{
    public required string Query { get; init; }
}