namespace CargoApp.Modules.Contracts.Cargoes;

public record CompanyDto(Guid Id, Guid CompanyId, string CompanyName, IList<DriverDto> Drivers);