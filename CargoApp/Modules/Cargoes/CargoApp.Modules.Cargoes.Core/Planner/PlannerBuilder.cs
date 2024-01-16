using CargoApp.Modules.Cargoes.Core.Planner.ExternalServices;
using Result;

namespace CargoApp.Modules.Cargoes.Core.Planner;

public class PlannerBuilder
{
    private Planner _planner;

    private PlannerBuilder()
    {
        _planner = new Planner();
    }
    
    public static PlannerBuilder Create()
    {
        return new PlannerBuilder();
    }
    
    public void AddRouteEngine(IRouteEngineClient routeEngineClient)
    {
        _planner.RouteEngineClient = routeEngineClient;
    }
    
    public void AddDrivers(List<Driver> drivers)
    {
        _planner.Drivers = drivers;
    }
    
    public Result<Planner> Build()
    {
        if (!_planner.Validate())
        {
            return "Planner is not valid";
        }

        return _planner;
    }
}