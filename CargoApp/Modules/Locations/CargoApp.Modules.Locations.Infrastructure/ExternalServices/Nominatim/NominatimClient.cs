using System.Text.Json;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.ExternalServices.Locations;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.DTO;
using CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim.Mappers;
using Microsoft.AspNetCore.WebUtilities;
using Result.ApiResult;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices.Nominatim;

internal class NominatimClient : ILocationClient
{
    private readonly HttpClient _httpClient;

    public NominatimClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }


    public async Task<ApiResult<IEnumerable<LocationDto>>> Search(string query, CancellationToken cancellationToken)
    {
        var request = GetHttpSearchRequest(query);
        using var result = await _httpClient
            .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);
        if (!result.IsSuccessStatusCode)
        {
            return ApiResult<IEnumerable<LocationDto>>.Fail("Something went wrong");
        }

        await using var contentStream = await result.Content.ReadAsStreamAsync(cancellationToken);
        var deserializedResult = await JsonSerializer.DeserializeAsync<List<NominatimLocation>>(contentStream,
            DefaultJsonSerializerOptions.Options, cancellationToken);
        if (deserializedResult is null)
        {
            return ApiResult<IEnumerable<LocationDto>>.Fail("Something went wrong");
        }

        return ApiResult<IEnumerable<LocationDto>>.Success(deserializedResult.Select(x => x.AsLocationDto()));
    }

    private static HttpRequestMessage GetHttpSearchRequest(string query)
    {
        // TODO: To config or settings
        var queryString = QueryHelpers.AddQueryString("search", new Dictionary<string, string?>
        {
            { "q", query },
            { "format", "json" },
            { "addressdetails", "1" }
        });
        // var uriBuilder = new UriBuilder("search");
        // var queryString = HttpUtility.ParseQueryString("search");
        // queryString["q"] = query;
        // queryString["format"] = "json";
        // queryString["addressdetails"] = "1";
        // uriBuilder.Query = queryString.ToString();

        var request = new HttpRequestMessage(HttpMethod.Get, queryString);
        request.Headers.Add("User-Agent", "Other");
        return request;
    }
}