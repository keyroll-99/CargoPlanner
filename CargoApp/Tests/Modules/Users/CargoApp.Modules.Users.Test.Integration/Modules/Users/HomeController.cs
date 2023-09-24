using System.Net;
using CargoApp.IntegrationTests;
using FluentAssertions;
using Xunit;

namespace CargoApp.Modules.Users.Test.Integration.Modules.Users;

public class HomeController : BaseControllerTest
{
    [Fact]
    public async Task get_base_endpoint_should_return_200_ok_and_module_name()
    {

        var response = await Client.GetAsync("/Users");

        response.StatusCode.Should().Be(HttpStatusCode.OK);
        
        var content = await response.Content.ReadAsStringAsync();
        content.Should().Be("Ok from Users"); 
    }

    public HomeController(OptionsProvider optionsProvider) : base(optionsProvider)
    { 
    }
}