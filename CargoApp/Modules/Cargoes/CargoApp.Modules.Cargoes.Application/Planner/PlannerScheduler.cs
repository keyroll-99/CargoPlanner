using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Quartz;

namespace CargoApp.Modules.Cargoes.Application.Planner;

public class PlannerScheduler : IJob
{
    private readonly IServiceProvider _serviceProvider;

    public PlannerScheduler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        AsyncServiceScope scope = _serviceProvider.CreateAsyncScope();
        ICargoRepository cargoRepository = scope.ServiceProvider.GetRequiredService<ICargoRepository>();
        ICompanyRepository companyRepository = scope.ServiceProvider.GetRequiredService<ICompanyRepository>();

        List<Core.CompanyAggregate.Company> companies = await companyRepository.GetAll();
        foreach (var company in companies)
        {
            var cargoToPlan = await cargoRepository.GetAllToPlanForCompany(company.CompanyId);
            
            
        }
    }

}