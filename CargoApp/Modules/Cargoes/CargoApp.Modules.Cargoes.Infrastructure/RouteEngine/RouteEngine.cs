using System.Text.Json;
using CargoApp.Modules.Cargoes.Core.Planner.RouteEngine;
using Microsoft.AspNetCore.Http;

namespace CargoApp.Modules.Cargoes.Infrastructure.RouteEngine;

public class RouteEngine : IRouteEngine
{
    private HttpClient _httpClient;

    public RouteEngine(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<double> GetDistanceAsync((double lat, double lon) from, (double lat, double lon) to)
    {
        var request = GenerateHttpRequest(from, to);
        using var response = await _httpClient.SendAsync(request);
        await using var responseStream = await response.Content.ReadAsStreamAsync();
        var responseBody = await JsonSerializer.DeserializeAsync<RouteDto>(responseStream);
        return responseBody.Routes.First().Distance;
    }
    
    private HttpRequestMessage GenerateHttpRequest((double lat, double lon) from, (double lat, double lon) to)
    {
        return new HttpRequestMessage(HttpMethod.Get, new Uri($"/route/v1/driving/{from.lon},{from.lat};{to.lon},{to.lat}"));
    }
}