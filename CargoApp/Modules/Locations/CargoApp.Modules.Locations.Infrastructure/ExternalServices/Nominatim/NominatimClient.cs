using System.Text.Json;
using System.Web;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Locations.Application.ExternalServices;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Core.Entities;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.Mappers;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim;

internal class NominatimClient : ILocationClient
{

    private readonly HttpClient _httpClient;

    public NominatimClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<Result<IEnumerable<Location>>> Search(string query, CancellationToken cancellationToken)
    {
        var request = GetHttpSearchRequest(query);
        using var result = await _httpClient.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            return "Something went wrong";
        }
        
        
        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var deserializedResult = (await JsonSerializer.DeserializeAsync<List<NominatimLocation>>(contentStream, DefaultJsonSerializerOptions.Options, cancellationToken))?.Select(x => x.AsLocation());
        if (deserializedResult is null)
        {
            return "Something went wrong";
        }

        return Result<IEnumerable<Location>>.Success(deserializedResult);
    }

    private static HttpRequestMessage GetHttpSearchRequest(string query)
    {
        // TODO: To config or settings
        var uriBuilder = new UriBuilder("/search");
        var queryString = HttpUtility.ParseQueryString(uriBuilder.Query);
        queryString["q"] = query;
        queryString["format"] = "json";
        queryString["addressdetails"] = "1";
        uriBuilder.Query = queryString.ToString();
        
        return new HttpRequestMessage(HttpMethod.Get, uriBuilder.ToString());
    }
}