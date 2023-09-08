using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.DTO;
using MediatR;

namespace CargoApp.Modules.Locations.Application.Queries.GetAllLocations;

public class GetAllLocationQuery : IRequest<Result<IEnumerable<LocationDto>, string>>
{
    
}