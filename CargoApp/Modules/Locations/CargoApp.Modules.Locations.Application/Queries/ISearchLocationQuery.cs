using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Queries;

public interface ISearchLocationQuery
{
    public Task<IEnumerable<Location>> Search(string query);
}