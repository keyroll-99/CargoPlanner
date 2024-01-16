using CargoApp.Modules.Cargoes.Core.Planner.ExternalServices;

namespace CargoApp.Modules.Cargoes.Infrastructure.ExternalService;

public class OsrmClient : IRouteEngineClient
{
    private readonly HttpClient _httpClient;

    public OsrmClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}