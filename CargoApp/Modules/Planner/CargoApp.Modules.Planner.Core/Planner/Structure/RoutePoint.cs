namespace CargoApp.Modules.Planner.Core.Planner.Structure;

public class RoutePoint
{
    public required Location Location { get; init; }
    private List<RoutePoint> Neighbours { get; set; } = new();

    public void AddNeighbour(RoutePoint neighbour)
    {
        Neighbours.Add(neighbour);
    }
}