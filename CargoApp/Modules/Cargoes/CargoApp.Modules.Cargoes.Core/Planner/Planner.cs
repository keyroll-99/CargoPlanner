using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;

namespace CargoApp.Modules.Cargoes.Core.Planner;

public class Planner
{
    private readonly IEnumerable<Graph.Graph> Graphs;
    private readonly Company _company;
    
    public Planner(IEnumerable<Graph.Graph> graphs, Company company)
    {
        Graphs = graphs;
    }
    
    public IEnumerable<Cargo> Plan()
    {
        List<Cargo> cargoes = new();
        Dictionary<Driver, double> driverCosts = new();
        foreach (var graph in Graphs)
        {
            
        }
        return cargoes;
    }
}