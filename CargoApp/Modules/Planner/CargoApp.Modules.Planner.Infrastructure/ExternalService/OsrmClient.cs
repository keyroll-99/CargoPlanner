using System.Net.Http.Headers;
using System.Text.Json;
using CargoApp.Modules.Planner.Core.Planner;
using CargoApp.Modules.Planner.Core.Planner.Exception;
using CargoApp.Modules.Planner.Core.Planner.ExternalService;

namespace CargoApp.Modules.Planner.Infrastructure.ExternalService;

public class OsrmClient : IRouteEngine
{
    private readonly HttpClient _httpClient;
    private static readonly JsonSerializerOptions JsonSerializerOptions = new()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public OsrmClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<RouteStatsResponse> GetRouteStats(Location start, Location end)
    {
        var request = CreateRequest(start, end);
        using var response = await _httpClient.SendAsync(request);
        if (!response.IsSuccessStatusCode)
        {
            throw new RouteServiceUnavailableException();
        }

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<RouteStatsResponse>(responseStream, JsonSerializerOptions);
    }
    
    private HttpRequestMessage CreateRequest(Location start, Location end)
    {
        var request = new HttpRequestMessage(HttpMethod.Get, $"route/v1/driving/{start.Lon},{start.Lat};{end.Lon},{end.Lat}");
        request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        return request;
    }


}