using CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Result.Extensions;

namespace CargoApp.Modules.Cargoes.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[controller]/[action]")]
[ApiController]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CargoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCargo(CreateCargoCommand command)
    {
        var result = await _mediator.Send(command);

        return result.ToApiResult().GetObjectResult();
    }
}