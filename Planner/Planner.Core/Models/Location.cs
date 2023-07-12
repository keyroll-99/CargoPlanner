namespace Planner.Core.Models;

public sealed class Location
{
    public double Lat { get; private set; }
    public double Lon { get; private set; }

    public Location(double lat, double lon)
    {
        Lat = lat;
        Lon = lon;
    }
}