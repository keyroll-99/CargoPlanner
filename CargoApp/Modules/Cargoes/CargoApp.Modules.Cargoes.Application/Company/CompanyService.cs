using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using CargoApp.Modules.Contracts.Cargoes.Services;

namespace CargoApp.Modules.Cargoes.Application.Company;

public class CompanyService : ICompanyService
{
    private readonly ICompanyRepository _companyRepository;

    public CompanyService(ICompanyRepository companyRepository)
    {
        _companyRepository = companyRepository;
    }

    public async Task<IList<CompanyDto>> GetAll()
    {
        return (await  _companyRepository.GetAll()).Select(c => c.CreateDto()).ToList();
    }
}