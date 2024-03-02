namespace CargoApp.Modules.Cargoes.Infrastructure.RouteEngine;

public class RouteDto
{
    public string Code { get; set; }
    public List<Route> Routes { get; set; }
    public List<Waypoint> Waypoints { get; set; }
}

public class Route
{
    public string Geometry { get; set; }
    public List<Leg> Legs { get; set; }
    public double Distance { get; set; }
    public int Duration { get; set; }
    public string WeightName { get; set; }
    public double Weight { get; set; }
}

public class Leg
{
    public List<object> Steps { get; set; }
    public double Distance { get; set; }
    public int Duration { get; set; }
    public string Summary { get; set; }
    public double Weight { get; set; }
}

public class Waypoint
{
    public string Hint { get; set; }
    public double Distance { get; set; }
    public string Name { get; set; }
    public List<double> Location { get; set; }
}