using System.Text.Json;

namespace CargoApp.Modules.Locations.Infrastructure.ExternalServices;

public class DefaultJsonSerializerOptions
{
    public static JsonSerializerOptions Options => new() {PropertyNameCaseInsensitive = true};

}