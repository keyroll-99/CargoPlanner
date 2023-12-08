namespace CargoApp.Modules.Contracts.Cargoes;

public record CargoDto(
    LocationDto From,
    LocationDto To,
    CompanyDto Company,
    DriverDto? Driver,
    DateTime CreatedAt,
    DateTime ExpectedDeliveryTime,
    bool IsDelivered,
    bool IsCancelled,
    Guid Id)
{
}