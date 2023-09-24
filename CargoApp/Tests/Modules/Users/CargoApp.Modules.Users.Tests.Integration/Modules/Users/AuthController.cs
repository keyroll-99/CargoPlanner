using System.Net;
using System.Net.Http.Json;
using CargoApp.Core.Infrastructure.Clock;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Integration.Modules.Users;

public class AuthController : BaseControllerTest, IDisposable
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
        _testDatabase.UserDbContext.Users.FirstOrDefaultAsync(x => x.Email == command.Email).Should().NotBeNull();
    }

    [Fact]
    public async Task sign_in_should_return_200_ok_and_user_data()
    {
        // Arrange
        var passwordManager = new PasswordHasher<User>();
        const string password = "sercet";
        const string email = "test@email.com";
        var user = new User
        {
            Email = email,
            Id = Guid.NewGuid(),
            CreateAt = DateTime.UtcNow,
            IsActive = true,
            PermissionMask = PermissionEnum.Admin,
            RefreshTokens = new List<RefreshToken>(),
            Password = passwordManager.HashPassword(null, password)
        };
        //
        await _testDatabase.UserDbContext.Users.AddAsync(user);
        await _testDatabase.UserDbContext.SaveChangesAsync();

        // Act
        var response = await Client.PostAsJsonAsync("/Users/Auth/SignIn", new SingInCommand(email, password));
        
        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = response.Content;
    }

    private readonly TestDatabase _testDatabase;

    public AuthController(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}