using System.Reflection;
using System.Runtime.CompilerServices;
using CargoApp.Modules.Planner.Core.Planner.ExternalService;
using CargoApp.Modules.Planner.Core.Planner.Structure;

namespace CargoApp.Modules.Planner.Core.Planner;

public class Planner
{
    public required IList<Driver> Drivers { get; init; }
    public required IList<Cargo> Cargoes { get; init; }
    public required IRouteEngine RouteEngine { get; init; }
    
    public async Task Plan()
    {
        var cargos = Cargoes.OrderBy(x => x.ExpectedDeliveryTime).ToList();
        
    }
}