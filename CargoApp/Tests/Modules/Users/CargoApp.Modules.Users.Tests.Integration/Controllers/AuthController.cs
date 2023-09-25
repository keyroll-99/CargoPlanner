using System.Net;
using System.Net.Http.Json;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Contracts.Companies;
using CargoApp.Modules.Contracts.Companies.DTO;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Integration.Controllers;

public class AuthController : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private ICompany _companyServiceMock = Substitute.For<ICompany>();

    public AuthController(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_companyServiceMock);
    }

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
        var dbUser = await CreateUserAsync();
        _companyServiceMock.FindEmployeeCompany(Arg.Any<Guid>()).Returns(new Company(Guid.NewGuid(), "test"));

        // Act
        var response = await Client.PostAsJsonAsync("/Users/Auth/SignIn", new SingInCommand(dbUser.Email, "secret"));

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var body = await response.Content.ReadFromJsonAsync<UserDto>();
        body.Email.Should().Be(dbUser.Email);
    }

    [Fact]
    public async Task get_users_me_should_return_200_ok_and_user_data()
    {
        // Arrange
        var dbUser = await CreateUserAsync();
        Authorize(dbUser.Id, dbUser.Email, dbUser.PermissionMask);

        // Act
        var response = await Client.GetAsync("/Users/User/Me");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    private async Task<User> CreateUserAsync()
    {
        var passwordManager = new PasswordHasher<User>();
        const string password = "secret";
        const string email = "test@email.com";
        var user = new User
        {
            Email = email,
            Id = Guid.NewGuid(),
            CreateAt = DateTime.UtcNow,
            IsActive = true,
            EmployeeId = Guid.NewGuid(),
            PermissionMask = PermissionEnum.Admin,
            RefreshTokens = new List<RefreshToken>(),
            Password = passwordManager.HashPassword(null, password),
        };
        await _testDatabase.UserDbContext.Users.AddAsync(user);
        await _testDatabase.UserDbContext.SaveChangesAsync();

        return user;
    }
}