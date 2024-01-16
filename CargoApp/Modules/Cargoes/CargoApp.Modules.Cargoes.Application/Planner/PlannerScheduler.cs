using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Cargoes.Core.Planner;
using Quartz;

namespace CargoApp.Modules.Cargoes.Application.Planner;

public class PlannerScheduler : IJob
{
    private readonly ICargoRepository _cargoRepository;
    private readonly ICompanyRepository _companyRepository;
    
    public PlannerScheduler(ICargoRepository cargoRepository, ICompanyRepository companyRepository)
    {
        _cargoRepository = cargoRepository;
        _companyRepository = companyRepository;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var companies = await _companyRepository.GetAll();
        var plannerBuilder = PlannerBuilder.Create();
        foreach (var company in companies)
        {
            var cargoes = await _cargoRepository.GetAllToPlanForCompany(company.Id);
            // plannerBuilder
                // .AddDrivers(company.Drivers.Select(x => new Core.Planner.Driver(x.Id, ).ToList()));
        }
    }
}