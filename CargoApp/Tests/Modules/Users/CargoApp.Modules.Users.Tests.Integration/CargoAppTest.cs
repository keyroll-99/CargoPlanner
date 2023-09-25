using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CargoApp.Modules.Users.Tests.Integration;

internal sealed class CargoAppTest : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public CargoAppTest(Action<IServiceCollection>? services)
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("IntegrationTest");
            if (services is not null)
            {
                builder.ConfigureServices(services);
            }
            
        }).CreateClient();
    }
}