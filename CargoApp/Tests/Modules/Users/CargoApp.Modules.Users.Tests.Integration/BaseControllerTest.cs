using CargoApp.Core.Infrastructure.Postgres;
using CargoApp.IntegrationTests;
using Xunit;

namespace CargoApp.Modules.Users.Tests.Integration;

[Collection("Api")]
public abstract class BaseControllerTest : IClassFixture<OptionsProvider>
{
    internal readonly HttpClient Client;

    public BaseControllerTest(OptionsProvider optionsProvider)
    {
        var app = new CargoAppTest();
        Client = app.Client;

        var postgresOptions = optionsProvider.Get<PostgresOptions>("Postgres");
    }
}