using CargoApp.Modules.Planner.Core.Planner.ExternalService;

namespace CargoApp.Modules.Planner.Infrastructure.ExternalService;

public class OsrmClient : IRouteEngine
{
    private readonly HttpClient _httpClient;

    public OsrmClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
}