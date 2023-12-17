using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CargoApp.Modules.Users.Core.DAL.SeedData;

internal class SeedData : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IWebHostEnvironment _environment;

    public SeedData(IServiceProvider serviceProvider, IWebHostEnvironment environment)
    {
        _serviceProvider = serviceProvider;
        _environment = environment;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_environment.IsDevelopment()){
            return;
        }
        
        var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<UserDbContext>();

        if (await context.Users.AnyAsync(cancellationToken: cancellationToken))
        {
            return;
        }
        
        await context.Users.AddAsync(UserData.User, cancellationToken);

        await context.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}