using CargoApp.Core.Infrastructure.CQRS.Query;
using CargoApp.Modules.Locations.Application.DTO;

namespace CargoApp.Modules.Locations.Application.Queries.GetAllLocation;

public interface IGetAllLocationHandler : IQueryHandler<GetAllLocationQuery, IEnumerable<LocationDto>, string>
{
    
}