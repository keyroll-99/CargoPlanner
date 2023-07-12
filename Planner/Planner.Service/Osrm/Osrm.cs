using Planner.Core.Http;
using Planner.Core.Models;

namespace Planner.Service.Osrm;

public class Osrm
{
    public async Task<Route> GetRoute(Location from, Location to)
    {
        var client = Connection.GetOsrmHttpClient();
        var response = await client.SendAsync(GetRequest(from, to));
        response.Content.
    }

    private static HttpRequestMessage GetRequest(Location from, Location to)
    {
        return new HttpRequestMessage(HttpMethod.Get, $"{from.Lon},{from.Lat};{to.Lon},{to.Lat}");
    }
}