using CargoApp.Core.Abstraction.QueueMessages;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Companies.Core.Repositories;
using CargoApp.Modules.Contracts.Events.Companies;
using MediatR;

namespace CargoApp.Modules.Companies.Core.Commands.FireEmployee;

internal class FireEmployeeCommandHandler: IRequestHandler<FireEmployeeCommand, Result>
{
    private readonly IEmployeeRepository _employeeRepository;
    private readonly IEventManager _eventManager;
    
    public FireEmployeeCommandHandler(IEmployeeRepository employeeRepository, IEventManager eventManager)
    {
        _employeeRepository = employeeRepository;
        _eventManager = eventManager;
    }

    public async Task<Result> Handle(FireEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = await _employeeRepository.GetByIdAsync(request.EmployeeId);
        if (employee is null)
        {
            return Result.Fail("User not found");
        }

        employee.IsActive = false;
        await _employeeRepository.UpdateAsync(employee);
        
        _eventManager.PublishEvent(new EmployeeFiredEvent(employee.Id));

        return Result.Success();
    }
}