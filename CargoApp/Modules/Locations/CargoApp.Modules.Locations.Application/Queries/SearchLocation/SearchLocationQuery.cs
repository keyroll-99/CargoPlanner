using CargoApp.Core.Infrastructure.CQRS.Query;

namespace CargoApp.Modules.Locations.Application.Queries.SearchLocation;

public class SearchLocationQuery : IQuery
{
    public required string Query { get; init; }
}