namespace CargoApp.Modules.Cargoes.Core.PlannerAggregate;

public class PlannedCargo
{
    public Guid Id { get; }
    public Route Route { get; }

    public PlannedCargo(Guid id, Route route)
    {
        Id = id;
        Route = route;
    }
}