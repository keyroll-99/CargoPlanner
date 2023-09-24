using System.Net;
using System.Net.Http.Json;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Users.Core.Commands;
using FluentAssertions;
using Xunit;

namespace CargoApp.Modules.Users.Test.Integration.Modules.Users;

public class AuthController : BaseControllerTest
{
    [Fact]
    public async Task when_create_user_and_user_doesnt_exist_in_db_then_create_user()
    {
        // Arrange
        var command = new CreateUserCommand("email@email.com", "test123123");

        // Act
        var response = await Client.PostAsJsonAsync("/Users/Auth/CreateUser", command);
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
    }

    public AuthController(OptionsProvider optionsProvider) : base(optionsProvider)
    {
    }
}