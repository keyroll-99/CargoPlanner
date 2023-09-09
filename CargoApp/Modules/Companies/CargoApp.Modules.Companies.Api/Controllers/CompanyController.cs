using CargoApp.Core.Infrastructure.Auth;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Modules.Companies.Core.Commands.CreateCompany;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Companies.Api.Controllers;

[ApiController]
[RequirePermission(PermissionEnum.Admin)]
[Route($"{ModuleInstaller.BasePath}/[action]")]
public class CompanyController : ControllerBase
{
    private readonly IMediator _mediator;

    public CompanyController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [ProducesResponseType(typeof(string), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [HttpPost]
    public async Task<IActionResult> CreateCompany(CreateCompanyCommand command)
    {
        var result = await _mediator.Send(command);

        return result.GetObjectResult();
    }
}