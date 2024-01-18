using CargoApp.Modules.Planner.Core.Planner.ExternalService;

namespace CargoApp.Modules.Planner.Application.Abstract;

public interface IRouteEngineFactory
{
    public IRouteEngine Create();
}