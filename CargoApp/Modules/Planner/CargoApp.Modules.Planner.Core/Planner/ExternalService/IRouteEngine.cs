using CargoApp.Modules.Planner.Core.Planner.Structure;

namespace CargoApp.Modules.Planner.Core.Planner.ExternalService;

public interface IRouteEngine
{
    public Task<RouteStatsResponse> GetRouteStats(Location start, Location end);
}