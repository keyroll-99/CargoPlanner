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

        await Core.DriverAggregate.Driver.Create(null, company, @event.Id).OnSuccessAsync(async (driver) =>
        {
            company.AddDriver(driver);
            await companyRepository.UpdateAsync(company);
        });
        
    }
}