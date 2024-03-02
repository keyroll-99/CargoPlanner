using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.Planner.Graph;
using CargoApp.Modules.Cargoes.Core.Planner.RouteEngine;
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
        IRouteEngine routeEngine = scope.ServiceProvider.GetRequiredService<IRouteEngine>();

        List<Core.CompanyAggregate.Company> companies = await companyRepository.GetAll();
        foreach (var company in companies)
        {
            List<Core.CargoAggregate.Cargo> cargoToPlan = await cargoRepository.GetAllToPlanForCompany(company.CompanyId);
            IEnumerable<Graph> graphs = cargoToPlan.Select(g => new Graph(g, routeEngine));
            Core.Planner.Planner planner = new(graphs, company);
            IEnumerable<Core.CargoAggregate.Cargo> result = planner.Plan();
            foreach (var cargo in result)
            {
                await cargoRepository.UpdateAsync(cargo);
            }
        }
    }

}