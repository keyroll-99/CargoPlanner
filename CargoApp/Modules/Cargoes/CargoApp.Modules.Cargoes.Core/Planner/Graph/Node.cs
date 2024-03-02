using CargoApp.Modules.Cargoes.Core.CargoAggregate;

namespace CargoApp.Modules.Cargoes.Core.Planner.Graph;

public class Node
{
    
    public List<Node> Neighbours { get; private set; } = new List<Node>();
    public Location Location { get; private set; }
    
    public Node(Location location)
    {
        Location = location;
    }
}