using CargoApp.Core.ShareCore.Entites;

namespace CargoApp.Modules.Locations.Core.Entities;

public class Location : BaseEntity
{
    public double Lat { get; private set; }
    public double Lon { get; private set; }
    public string Name { get; private set; }
    public string DisplayName { get; private set; }
    public long OsmId { get; private set;}

    public Location(Guid id, DateTime createAt, double lat, double lon, string name, string displayName, long osmId) : base(id, createAt)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        DisplayName = displayName;
        OsmId = osmId;
    }

    public Location(double lat, double lon, string name, string displayName, long osmId)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        DisplayName = displayName;
        OsmId = osmId;
    }
}