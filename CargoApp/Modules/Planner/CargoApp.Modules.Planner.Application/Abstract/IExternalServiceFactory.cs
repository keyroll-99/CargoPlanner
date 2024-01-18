using CargoApp.Modules.Contracts.Cargoes.Services;

namespace CargoApp.Modules.Planner.Application.Abstract;

internal interface IExternalServiceFactory
{
    ICompanyService GetCompanyService();
    ICargoService GetCargoService();
}