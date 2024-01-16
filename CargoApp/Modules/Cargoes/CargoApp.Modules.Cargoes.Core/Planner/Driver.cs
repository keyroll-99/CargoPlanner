using CargoApp.Modules.Cargoes.Core.LocationAggregate;

namespace CargoApp.Modules.Cargoes.Core.Planner;

public class Driver
{
    public Driver(Guid id, Location startLocation, Location endLocation)
    {
        Id = id;
        StartLocation = startLocation;
        EndLocation = endLocation;
    }

    public Guid Id { get; private set; }
    public Location StartLocation { get; private set; }
    public Location EndLocation { get; private set; }
 
}