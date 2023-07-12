namespace Planner.Core.Models;

public class Route
{
    public Location From { get; set; }
    public Location To { get; set; }
    public double Distance { get; set; }
}