namespace CargoApp.Modules.Cargoes.Core.PlannerAggregate;

public class Driver
{
    public Guid Id { get; init; }
    public RoutePoint StartLocation { get; init; }
    public RoutePoint FinishLocation { get; init; }
    public ICollection<PlannedCargo> PlannedCargoes { get; private set; } = new List<PlannedCargo>();
}