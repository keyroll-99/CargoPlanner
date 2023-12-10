using System.Net;
using System.Net.Http.Json;
using CargoApp.Core.Abstraction.Mail;
using CargoApp.Core.Infrastructure.Clock;
using CargoApp.IntegrationTests;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.EmailTemplates.PasswordRecovery;
using CargoApp.Modules.Users.Core.Entities;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Integration.Controllers;

public class PasswordRecoveryController : BaseControllerTest, IDisposable
{
    private readonly TestDatabase _testDatabase;
    private readonly IMailManager _mailManagerMock = Substitute.For<IMailManager>();

    public PasswordRecoveryController(OptionsProvider optionsProvider) : base(optionsProvider)
    {
        _testDatabase = new TestDatabase();
    }

    protected override void ConfigureServices(IServiceCollection services)
    {
        services.AddSingleton(_mailManagerMock);
    }

    [Fact]
    public async Task when_user_doesnt_exists_with_email_then_return_error()
    {
        // Arrange
        var command = new InitPasswordRecoveryCommand("test");

        // Act
        var result = await Client.PostAsJsonAsync("/Users/PasswordRecovery/InitPasswordRecovery", command);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task when_user_exits_then_create_password_recovery_record_send_email_with_url()
    {
        // Arrange
        var user = await _testDatabase.CreateUserAsync();
        var command = new InitPasswordRecoveryCommand(user.Email);

        // Act
        var result = await Client.PostAsJsonAsync("/Users/PasswordRecovery/InitPasswordRecovery", command);

        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
        var passwordRecoveryModel =
            await _testDatabase.UserDbContext.PasswordRecoveries.FirstOrDefaultAsync(x => x.UserId == user.Id);
        passwordRecoveryModel.Should().NotBeNull();

        await _mailManagerMock.Received(1).SendMailAsync<PasswordRecoveryMail>(
            Arg.Is<MailModel>(model => model.To == command.Email && model.Subject == "Password recovery"),
            Arg.Is<PasswordRecoveryMail>(x => x.Hash == passwordRecoveryModel.Id.ToString()));
    }

    [Fact]
    public async Task when_given_wrong_recovery_key_then_return_400_bad_request()
    {
        // Arrange
        var recoveryKey = Guid.NewGuid();

        // Act
        var response = await Client.GetAsync($"/Users/PasswordRecovery/IsRecoveryKeyValid?recoveryKey={recoveryKey}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    [Fact]
    public async Task when_given_correct_recovery_key_then_return_204_no_content()
    {
        // Arrange
        var user = await _testDatabase.CreateUserAsync();
        var recoveryKey = Guid.NewGuid();
        var recoveryModel = PasswordRecovery.CreatePasswordRecovery(user.Id, new Clock());
        recoveryModel.Id = recoveryKey;
        _testDatabase.UserDbContext.PasswordRecoveries.Add(recoveryModel);

        await _testDatabase.UserDbContext.SaveChangesAsync();

        // Act
        var response = await Client.GetAsync($"/Users/PasswordRecovery/IsRecoveryKeyValid?recoveryKey={recoveryKey}");

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Theory]
    [InlineData("pass", "Password is not strong enough")]
    [InlineData("new-super_password", "Invalid recovery key")]
    public async Task change_password_when_given_invalid_data_then_return_400_bad_request(string password, string error)
    {
        // Arrange
        var user = await _testDatabase.CreateUserAsync();
        var recoveryModel = PasswordRecovery.CreatePasswordRecovery(user.Id, new Clock());
        _testDatabase.UserDbContext.PasswordRecoveries.Add(recoveryModel);
        await _testDatabase.UserDbContext.SaveChangesAsync();

        var command = new ChangePasswordCommand(password);

        // Act
        var result =
            await Client.PatchAsJsonAsync($"/Users/PasswordRecovery/ChangePassword/{Guid.NewGuid()}", command);
        
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var body = (await result.Content.ReadAsStringAsync());
        body.Should().Be(error);
    }
    
    [Fact]
    public async Task change_password_when_given_correct_data_then_return_204_no_content()
    {
        // Arrange
        var user = await _testDatabase.CreateUserAsync();
        var recoveryModel = PasswordRecovery.CreatePasswordRecovery(user.Id, new Clock());
        _testDatabase.UserDbContext.PasswordRecoveries.Add(recoveryModel);
        await _testDatabase.UserDbContext.SaveChangesAsync();

        var command = new ChangePasswordCommand("new-super_password");

        // Act
        var result =
            await Client.PatchAsJsonAsync($"/Users/PasswordRecovery/ChangePassword/{recoveryModel.Id}", command);
        
        // Assert
        result.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    public void Dispose()
    {
        _testDatabase.Dispose();
    }
}