using CargoApp.Core.ShareCore.Policies;
using CargoApp.Modules.Companies.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Serilog;

namespace CargoApp.Modules.Companies.Core.Commands.AddWorker.Policy;

internal class UniqueMailPolicy : IPolicy<CreateEmployeeCommand>
{
    public string ErrorMessage => "User with this mail exists";
    public int StatusCode => StatusCodes.Status400BadRequest;

    private readonly ILogger _logger;
    private readonly IEmployeeRepository _employeeRepository;


    public UniqueMailPolicy(ILogger logger, IEmployeeRepository employeeRepository)
    {
        _logger = logger;
        _employeeRepository = employeeRepository;
    }


    public bool IsApplicable(CreateEmployeeCommand model)
        => true;

    public async ValueTask<bool> IsValidAsync(CreateEmployeeCommand model)
    {
        var exists = await _employeeRepository.ExistsByEmail(model.Email);
        if (exists)
        {
            _logger.Information($"Cannot create user with email {model.Email}, because user with this mail exists");
        }

        return !exists;
    }
}