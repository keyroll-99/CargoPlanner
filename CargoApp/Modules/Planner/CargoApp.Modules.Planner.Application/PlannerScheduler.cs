using CargoApp.Modules.Contracts.Cargoes.Services;
using Quartz;

namespace CargoApp.Modules.Planner.Application;

public class PlannerScheduler : IJob
{
    private readonly IServiceProvider _serviceProvider;

    public PlannerScheduler(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public Task Execute(IJobExecutionContext context)
    {
        throw new NotImplementedException();
    }
}