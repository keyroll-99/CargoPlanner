using CargoApp.Module.Contracts.Companies.DTO;

namespace CargoApp.Module.Contracts.Companies;

public interface ICompany
{
    Task<Company?> FindEmployeeCompany(Guid employeeId);
}