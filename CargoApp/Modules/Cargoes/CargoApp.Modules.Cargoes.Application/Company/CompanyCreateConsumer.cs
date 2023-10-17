using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Contracts.Events.Companies;
using Serilog;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;

namespace CargoApp.Modules.Cargoes.Application.Company;

internal class CompanyCreateConsumer : IEventConsumer<CompanyCreateEvent>
{
    private readonly ILogger _logger;
    private readonly IServiceProvider _serviceProvider;

    public CompanyCreateConsumer(ILogger logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    public async Task Process(CompanyCreateEvent @event)
    {
        var company = Core.CompanyAggregate.Company.Create(@event.Name, @event.Id);

        var companyRepository = await _serviceProvider.GetService<ICompanyRepository>();

        await companyRepository.AddAsync(company);
        _logger.Information("Company {name} create at cargoes module", @event.Name);
    }
}