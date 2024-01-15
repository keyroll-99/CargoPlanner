using Quartz;

namespace CargoApp.Modules.Cargoes.Application.Planner;

public class TestSchduler : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        return Task.CompletedTask;
    }
}