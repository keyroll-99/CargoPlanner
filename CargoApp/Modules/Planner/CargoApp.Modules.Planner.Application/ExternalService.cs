using CargoApp.Modules.Contracts.Cargoes.Services;
using CargoApp.Modules.Planner.Application.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Modules.Planner.Application;

internal class ExternalService : IExternalServiceFactory
{
    private readonly IServiceProvider _serviceProvider;

    public ExternalService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public ICompanyService GetCompanyService()
    {
        var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<ICompanyService>();
    }

    public ICargoService GetCargoService()
    {
        var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<ICargoService>();
    }

    public IDriverService GetDriverService()
    {
        var scope = _serviceProvider.CreateAsyncScope();
        return scope.ServiceProvider.GetRequiredService<IDriverService>();
    }
}