using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace CargoApp.Modules.Users.Test.Integration;

internal sealed class CargoAppTest : WebApplicationFactory<Program>
{
    public HttpClient Client { get; }
    
    public CargoAppTest()
    {
        Client = WithWebHostBuilder(builder =>
        {
            builder.UseEnvironment("IntegrationTest");
        }).CreateClient();
    }
}