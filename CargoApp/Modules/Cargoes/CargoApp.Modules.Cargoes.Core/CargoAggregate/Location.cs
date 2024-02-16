using CargoApp.Modules.Contracts.Cargoes;

namespace CargoApp.Modules.Cargoes.Core.CargoAggregate;

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
    
    public LocationDto CreateDto()
    {
        return new LocationDto(Id, Lat, Lon, Name, OsmId);
    }
}