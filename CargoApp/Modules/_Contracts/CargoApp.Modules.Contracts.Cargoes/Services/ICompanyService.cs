namespace CargoApp.Modules.Contracts.Cargoes.Services;

public interface ICompanyService
{
    Task<IList<CompanyDto>> GetAll();
}