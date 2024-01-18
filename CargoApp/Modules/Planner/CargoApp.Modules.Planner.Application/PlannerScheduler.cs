using CargoApp.Modules.Contracts.Cargoes;
using CargoApp.Modules.Contracts.Cargoes.Services;
using CargoApp.Modules.Planner.Application.Abstract;
using CargoApp.Modules.Planner.Application.Mapper;
using CargoApp.Modules.Planner.Core.Planner.ExternalService;
using Quartz;

namespace CargoApp.Modules.Planner.Application;

internal class PlannerScheduler : IJob
{
    private readonly IExternalServiceFactory _externalServiceFactory;
    private readonly IRouteEngineFactory _routeEngineFactory;

    public PlannerScheduler(IExternalServiceFactory externalServiceFactory, IRouteEngineFactory routeEngineFactory)
    {
        _externalServiceFactory = externalServiceFactory;
        _routeEngineFactory = routeEngineFactory;
    }

    public async Task Execute(IJobExecutionContext context)
    {
        var cargoService = _externalServiceFactory.GetCargoService();
        var companyService = _externalServiceFactory.GetCompanyService();
        var companies = await companyService.GetAll();
        var routeEngine = _routeEngineFactory.Create();
        await PlanRoutesForEachCompany(companies, cargoService, routeEngine);
    }

    private static async Task PlanRoutesForEachCompany(IList<CompanyDto> companies, ICargoService cargoService,
        IRouteEngine routeEngine)
    {
        foreach (var company in companies)
        {
            var cargoes = await cargoService.GetCargoesToPlan(company.Id);
            if (!ShouldRunPlannerForCompany(cargoes, company))
            {
                continue;
            }
            var planner = CreatePlanner(routeEngine, cargoes, company);
            await planner.Plan();
        }
    }

    private static Core.Planner.Planner CreatePlanner(IRouteEngine routeEngine, IList<CargoDto> cargoes, CompanyDto company)
    {
        var planner = new Core.Planner.Planner
        {
            Cargoes = cargoes.Select(c => c.AsCargo()).ToList(),
            Drivers = company.Drivers.Select(d => d.AsDriver()).ToList(),
            RouteEngine = routeEngine
        };
        return planner;
    }

    private static bool ShouldRunPlannerForCompany(IList<CargoDto> cargoes, CompanyDto company)
    {
        return cargoes.Any() && company.Drivers.Any();
    }
}