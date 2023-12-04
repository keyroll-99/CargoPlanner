using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Modules.Companies.Core.Repositories;
using CargoApp.Modules.Contracts.Events.Companies;
using MediatR;

namespace CargoApp.Modules.Companies.Core.Commands.FireEmployee;

internal class FireEmployeeCommandHandler: IRequestHandler<FireEmployeeCommand, CargoApp.Core.Infrastructure.Response.Result>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEventManager _eventManager;
    
    public FireEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEventManager eventManager)
    {
        _employeeRepository = employeeRepository;
        _eventManager = eventManager;
    }

    public async Task<CargoApp.Core.Infrastructure.Response.Result> Handle(FireEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee is null)
        {
            return CargoApp.Core.Infrastructure.Response.Result.Fail("User not found");
        }

        employee.IsActive = false;
        await _employeeRepository.UpdateAsync(employee);
        
        _eventManager.PublishEvent(new EmployeeFiredEvent(employee.Id));

        return CargoApp.Core.Infrastructure.Response.Result.Success();
    }
}