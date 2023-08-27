using CargoApp.Core.Infrastructure.CQRS.Query;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocations;

public class SearchLocationQuery : IQuery
{
    public required string Query { get; init; }
}