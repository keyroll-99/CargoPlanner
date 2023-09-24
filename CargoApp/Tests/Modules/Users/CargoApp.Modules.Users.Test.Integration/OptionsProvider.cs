using CargoApp.Core.Infrastructure;
using Microsoft.Extensions.Configuration;

namespace CargoApp.IntegrationTests;

public sealed class OptionsProvider
{
    private readonly IConfiguration _configuration;

    public OptionsProvider()
    {
        _configuration = GetConfigurationRoot();
    }

    public T Get<T>(string sectionName) where T : class, new() => _configuration.GetOptions<T>(sectionName);
    
    private static IConfigurationRoot GetConfigurationRoot()
    {
        return new ConfigurationBuilder()
            .AddJsonFile("appsettings.IntegrationTest.json", true)
            .AddEnvironmentVariables()
            .Build();
    }
}