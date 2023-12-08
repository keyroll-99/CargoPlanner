namespace CargoApp.Modules.Contracts.Cargoes;

public record LocationDto(Guid Id, double Lat, double Lon, string Name, long OsmId);