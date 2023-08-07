using CargoApp.Modules.Locations.Core.Entities;

namespace CargoApp.Modules.Locations.Application.Service;

public interface ISearchLocation
{
    public Task<IEnumerable<Location>> Search(string query);
}