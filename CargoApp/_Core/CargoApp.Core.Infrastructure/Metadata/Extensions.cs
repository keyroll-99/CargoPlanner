using Microsoft.Extensions.DependencyInjection;

namespace CargoApp.Core.Infrastructure.Metadata;

public static class Extensions
{
    private const string MetadataSectionName = "Metadata";
    
    public static IServiceCollection LoadMetadata(this IServiceCollection services)
    {
        var metadata = services.GetOptions<Metadata>(MetadataSectionName);
        services.AddSingleton(metadata);
        return services;

    }
}