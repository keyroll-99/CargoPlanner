using CargoApp.Modules.Planner.Application.Abstract;
using CargoApp.Modules.Planner.Core.Planner.ExternalService;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Planner.Infrastructure.ExternalService;

public class OsrmClientFactory : IRouteEngineFactory
{
    private readonly IServiceProvider _serviceProvider;

    public OsrmClientFactory(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IRouteEngine Create()
    {
        var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<IRouteEngine>();
    }
}