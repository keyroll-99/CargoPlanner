namespace CargoApp.Modules.Cargoes.Core.Planner.RouteEngine;

public interface IRouteEngine
{
    Task<double> GetDistanceAsync((double lat, double lon) from, (double lat, double lon) to);
}