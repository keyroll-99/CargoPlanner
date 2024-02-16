using CargoApp.Modules.Planner.Core.Planner.Structure;

namespace CargoApp.Modules.Planner.Core.Planner;

public class Driver
{
    public required Location Home { get; init; }
    public required Guid Id { get; init; }
    public required IList<Route> Routes { get; init; } = new List<Route>();
}