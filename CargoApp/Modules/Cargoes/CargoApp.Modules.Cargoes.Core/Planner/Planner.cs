using CargoApp.Modules.Cargoes.Core.Planner.ExternalServices;

namespace CargoApp.Modules.Cargoes.Core.Planner;

public class Planner
{
    public IRouteEngineClient RouteEngineClient { get; set; }
    public List<Driver> Drivers { get; set; }

    internal Planner()
    {
        
    }
    
    public bool Validate()
    {
        return RouteEngineClient is not null && Drivers is not null;
    }
}