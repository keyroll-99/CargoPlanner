namespace Planner.Core.Models;

public sealed class Driver
{
    public Guid Hash { get; private set; }
    public Location StartLocation { get; private set; }
    public Location StopLocation { get; private set; }

    public Driver(Guid hash, Location startLocation, Location stopLocation)
    {
        Hash = hash;
        StartLocation = startLocation;
        StopLocation = stopLocation;
    }
}