using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Modules.Cargoes.Core.CompanyAggregate;
using CargoApp.Modules.Cargoes.Core.DriverAggregate;
using CargoApp.Modules.Contracts.Events.Companies;
using CargoApp.Modules.Contracts.Events.Companies.Enums;
using IServiceProvider = CargoApp.Core.Abstraction.Services.IServiceProvider;

namespace CargoApp.Modules.Cargoes.Application.Driver;

public class EmployeeCreateConsumer : IEventConsumer<EmployeeCreateEvent>
{
    private readonly IServiceProvider _serviceProvider;

    public EmployeeCreateConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task Process(EmployeeCreateEvent @event)
    {
        if (!@event.WorkingPosition.HasFlag(WorkingPositionEnum.Driver))
        {
            return;
        }

        var companyRepository = await _serviceProvider.GetService<ICompanyRepository>();
        var company = await companyRepository.GetByCompanyId(@event.CompanyId);

        Core.DriverAggregate.Driver.Create(null, company).MatchOnlySuccess(async (driver) =>
        {
            var driverRepository = await _serviceProvider.GetService<IDriverRepository>();
            await driverRepository.AddAsync(driver);
        });
        
    }
}