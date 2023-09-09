using CargoApp.Core.Infrastructure.Auth;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Modules.Companies.Core.Commands.AddWorker;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Companies.Api.Controllers;

[ApiController]
[RequirePermission(PermissionEnum.Workers)]
[Route($"{ModuleInstaller.BasePath}/[controller]/[action]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;

    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddEmployee(CreateEmployeeCommand request)
    {
        var result = await _mediator.Send(request);
        return result.GetObjectResult();
    }
}