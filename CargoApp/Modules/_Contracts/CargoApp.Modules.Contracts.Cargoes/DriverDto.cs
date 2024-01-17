namespace CargoApp.Modules.Contracts.Cargoes;

public record DriverDto(Guid Id, LocationDto Home, bool IsActive);