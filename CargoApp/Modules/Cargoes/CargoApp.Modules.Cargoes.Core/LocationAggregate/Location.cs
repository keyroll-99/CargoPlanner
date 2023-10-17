namespace CargoApp.Modules.Cargoes.Core.LocationAggregate;

public class Location
{
    public Guid Id { get; init; }
    public double Lat { get; private set; }
    public double Lon { get; private set; }
    public string Name { get; private set; }
    public long OsmId { get; private set; }


    public Location(double lat, double lon, string name, long osmId)
    {
        Lat = lat;
        Lon = lon;
        Name = name;
        OsmId = osmId;
    }
}