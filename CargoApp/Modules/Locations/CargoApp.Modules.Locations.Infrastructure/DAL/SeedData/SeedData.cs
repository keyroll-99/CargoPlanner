using CargoApp.Core.ShareCore.Clock;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CargoApp.Modules.Locations.Infrastructure.DAL.SeedData;

internal class SeedData : IHostedService
{
    private readonly IServiceProvider _serviceProvider;
    private readonly IClock _clock;
    private readonly IWebHostEnvironment _environment;

    public SeedData(IServiceProvider serviceProvider, IClock clock, IWebHostEnvironment environment)
    {
        _serviceProvider = serviceProvider;
        _clock = clock;
        _environment = environment;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        if (!_environment.IsDevelopment())
        {
            return;
        }

        var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<LocationDbContext>();
        
        if (await context.Locations.AnyAsync(cancellationToken: cancellationToken))
        {
            return;
        }

        await context.Locations.AddRangeAsync(LocationData.Locations, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);

    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}