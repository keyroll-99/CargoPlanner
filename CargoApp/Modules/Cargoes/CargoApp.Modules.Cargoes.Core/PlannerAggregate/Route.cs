using CargoApp.Modules.Cargoes.Core.CargoAggregate;

namespace CargoApp.Modules.Cargoes.Core.PlannerAggregate;

public class Route
{
    private ICollection<RoutePoint> Points { get; }

    public Route(Cargo cargo, Driver? driver)
    {
        Points = new List<RoutePoint>();
        if (driver is not null)
        {
            Points.Add(driver.StartLocation);
        }
    }
}