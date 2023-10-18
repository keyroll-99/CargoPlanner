namespace CargoApp.Modules.Cargoes.Core.CompanyAggregate;

public interface ICompanyRepository
{
    Task AddAsync(Company company);
    Task<Company?> GetByCompanyId(Guid companyId);
}