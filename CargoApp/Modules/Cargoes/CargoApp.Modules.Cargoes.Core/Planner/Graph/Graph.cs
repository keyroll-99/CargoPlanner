using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.Planner.RouteEngine;

namespace CargoApp.Modules.Cargoes.Core.Planner.Graph;

public class Graph
{
    public Node StartNode { get; private set; }
    public Node EndNode { get; private set; }
    public Cargo Cargo { get; private set; }
    private bool HasDriver => Cargo.Driver is not null;
    private IRouteEngine _routeEngine;
    
    public Graph(Cargo cargo, IRouteEngine routeEngine)
    {
        Cargo = cargo;
        _routeEngine = routeEngine;
        InitStartNode();
        InitEndNode();
    }

    private void InitStartNode()
    {
        StartNode = new Node(Cargo.From);
    }
    
    private void InitEndNode()
    {
        EndNode = new Node(Cargo.To);
    }

    private double GetCotsForDriver(Driver driver, Cargo? prevCargo = null)
    {
        
    }
    
}