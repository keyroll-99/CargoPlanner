namespace CargoApp.Modules.Contracts.Events.Companies;

public record EmployeeCreateEvent(Guid Id, Guid CompanyId, string Name, string Surname, string Email);