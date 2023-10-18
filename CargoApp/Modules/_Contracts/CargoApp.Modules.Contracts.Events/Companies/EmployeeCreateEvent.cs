using CargoApp.Modules.Contracts.Events.Companies.Enums;

namespace CargoApp.Modules.Contracts.Events.Companies;

public record EmployeeCreateEvent(Guid Id, Guid CompanyId, string Name, string Surname, string Email, WorkingPositionEnum WorkingPosition);