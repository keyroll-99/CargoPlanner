using CargoApp.Modules.Locations.Application.Queries;
using CargoApp.Modules.Locations.Application.Queries.SearchLocation;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[action]")]
public class LocationController : ControllerBase
{
    private readonly IMediator _mediator;

    public LocationController(IMediator mediator)
    {
        _mediator = mediator;
    }


    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var result = await _mediator.Send(new SearchLocationQuery { Query = query });

        return result.GetObjectResult();
    }
}