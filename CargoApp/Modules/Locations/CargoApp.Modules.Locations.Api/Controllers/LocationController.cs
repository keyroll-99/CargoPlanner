using CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.Queries.SearchLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[action]")]
[Authorize]
public class LocationController : ControllerBase
{
    private readonly ISearchLocationQueryHandler _searchLocationQueryHandler;
    private readonly IAddLocationCommandHandler _addLocationCommandHandler;


    public LocationController(ISearchLocationQueryHandler searchLocationQueryHandler, IAddLocationCommandHandler addLocationCommandHandler)
    {
        _searchLocationQueryHandler = searchLocationQueryHandler;
        _addLocationCommandHandler = addLocationCommandHandler;
    }

    [HttpGet]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var result = await _searchLocationQueryHandler.Handle(new SearchLocationQuery{Query = query});

        return result.GetObjectResult();
    }

    [HttpPost]
    public async Task<IActionResult> Add(LocationDto newLocation)
    {
        var result = await _addLocationCommandHandler.Handle(new AddLocationCommand(newLocation));
        return result.GetObjectResult();
    }
}