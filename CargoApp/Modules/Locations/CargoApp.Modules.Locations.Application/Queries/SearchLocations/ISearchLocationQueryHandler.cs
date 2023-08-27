using CargoApp.Core.Infrastructure.CQRS.Query;
using CargoApp.Modules.Locations.Application.DTO;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocations;

public interface ISearchLocationQueryHandler : IQueryHandler<SearchLocationQuery, IEnumerable<LocationDto>, string>
{
    
}