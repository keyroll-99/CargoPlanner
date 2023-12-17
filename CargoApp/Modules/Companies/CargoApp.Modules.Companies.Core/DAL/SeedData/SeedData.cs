using CargoApp.Core.ShareCore.Clock;
using CargoApp.Modules.Companies.Core.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CargoApp.Modules.Companies.Core.DAL.SeedData;

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
            return;
        
        var scope = _serviceProvider.CreateAsyncScope();
        var context = scope.ServiceProvider.GetRequiredService<CompanyDbContext>();

        if (context.Companies.Any())
        {
            return;
        }

        var company = CompanyData.Company;
        
        company.Employees.AddRange(EmployeeData.Employees);
        
        await context.Companies.AddAsync(company, cancellationToken);
        
        await context.SaveChangesAsync(cancellationToken);
    }

    public Task StopAsync(CancellationToken cancellationToken)
        => Task.CompletedTask;
}