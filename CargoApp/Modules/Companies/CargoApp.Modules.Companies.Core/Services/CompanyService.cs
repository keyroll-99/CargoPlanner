using CargoApp.Module.Contracts.Companies;
using CargoApp.Module.Contracts.Companies.DTO;
using CargoApp.Modules.Companies.Core.Repositories;

namespace CargoApp.Modules.Companies.Core.Services;

internal class CompanyService : ICompany
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<Company?> FindEmployeeCompany(Guid employeeId)
    {
        var company = await _companyRepository.GetCompanyByEmployeeId(employeeId);

        return company is null ? null : new Company(company.Id, company.Name);
    }
}