using CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;
using CargoApp.Modules.Cargoes.Application.Cargo.FetchById;
using CargoApp.Modules.Cargoes.Application.Cargo.UpdateCargo;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Result.Extensions;

namespace CargoApp.Modules.Cargoes.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[controller]")]
[ApiController]
[Authorize]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CargoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Cargo), StatusCodes.Status200OK)]
    public async Task<IActionResult> FetchById(Guid id)
    {
        var result = await _mediator.Send(new FetchByIdCommand(id));
        return result.ToApiResult().GetObjectResult();
    }


    [HttpPost("[action]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    public async Task<IActionResult> CreateCargo(CreateCargoCommand command)
    {
        var result = await _mediator.Send(command);

        return result.ToApiResult().GetObjectResult();
    }

    [HttpPut("[action]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(Cargo), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCargo(UpdateCargoCommand command)
    {
        var result = (await _mediator.Send(command));
        return result.IsSuccess
            ? (await _mediator.Send(new FetchByIdCommand(command.Id))).GetObjectResult()
            : result.ToApiResult().GetObjectResult();
    }
}