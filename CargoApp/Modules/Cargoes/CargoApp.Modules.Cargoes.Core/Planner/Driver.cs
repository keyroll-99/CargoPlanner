using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.Planner;

public class Driver
{
    public Guid Id { get; private set; }
    public Location StartLocation { get; private set; }
    public Location EndLocation { get; private set; }
 
}