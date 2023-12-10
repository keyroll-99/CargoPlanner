using CargoApp.Core.Infrastructure.Auth;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Modules.Cargoes.Application.Cargo.CreateCargo;
using CargoApp.Modules.Cargoes.Application.Cargo.FetchById;
using CargoApp.Modules.Cargoes.Application.Cargo.FetchCargoesPage;
using CargoApp.Modules.Cargoes.Application.Cargo.UpdateCargo;
using CargoApp.Modules.Cargoes.Core.CargoAggregate;
using CargoApp.Modules.Contracts.Cargoes;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Result.Extensions;

namespace CargoApp.Modules.Cargoes.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[controller]")]
[ApiController]
[Authorize]
[RequirePermission(PermissionEnum.Cargoes)]
public class CargoController : ControllerBase
{
    private readonly IMediator _mediator;

    public CargoController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(CargoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> FetchById(Guid id)
    {
        var result = await _mediator.Send(new FetchByIdQuery(id));
        return result.ToApiResult().GetObjectResult();
    }
    
    
    [HttpGet("[action]")]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IList<CargoDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> FetchPage([FromQuery] int page, [FromQuery] int pageSize)
    {
        var result = await _mediator.Send(new FetchCargoesPageQuery(page, pageSize));
        return result.GetObjectResult();
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
    [ProducesResponseType(typeof(CargoDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> UpdateCargo(UpdateCargoCommand command)
    {
        var result = (await _mediator.Send(command));
        return result.IsSuccess
            ? (await _mediator.Send(new FetchByIdQuery(command.Id))).GetObjectResult()
            : result.ToApiResult().GetObjectResult();
    }
}