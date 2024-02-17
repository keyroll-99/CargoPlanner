namespace CargoApp.Modules.Cargoes.Core.PlannerAggregate;

// najpierw zbudować graf tras (jakie są ze sobą połączone) a  dopiero potem na podstawie mniejszych grafów szukać lepszego kierowcy
public class Planner
{
    public ICollection<Driver> AvailableDrivers { get; private set; }
    public ICollection<PlannedCargo> PlannedCargoes { get; private set; }

    public Planner(ICollection<Driver> availableDrivers, ICollection<PlannedCargo> plannedCargoes)
    {
        AvailableDrivers = availableDrivers;
        PlannedCargoes = plannedCargoes;
    }

    public Task AssignDriversToCargo()
    {
        return Task.CompletedTask;
    }
}