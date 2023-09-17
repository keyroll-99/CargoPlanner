using CargoApp.Modules.Contracts.Companies.DTO;

namespace CargoApp.Modules.Contracts.Companies;

public interface ICompany
{
    Task<Company?> FindEmployeeCompany(Guid employeeId);
}