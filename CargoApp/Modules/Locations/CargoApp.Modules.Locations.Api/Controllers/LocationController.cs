using CargoApp.Modules.Locations.Application.Commands.AddLocationCommand;
using CargoApp.Modules.Locations.Application.DTO;
using CargoApp.Modules.Locations.Application.Queries.GetAllLocation;
using CargoApp.Modules.Locations.Application.Queries.SearchLocation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[action]")]
[Authorize]
public class LocationController : ControllerBase
{
    private readonly ISearchLocationQueryHandler _searchLocationQueryHandler;
    private readonly IAddLocationCommandHandler _addLocationCommandHandler;
    private readonly IGetAllLocationHandler _getAllLocationHandler;


    public LocationController(ISearchLocationQueryHandler searchLocationQueryHandler, IAddLocationCommandHandler addLocationCommandHandler, IGetAllLocationHandler getAllLocationHandler)
    {
        _searchLocationQueryHandler = searchLocationQueryHandler;
        _addLocationCommandHandler = addLocationCommandHandler;
        _getAllLocationHandler = getAllLocationHandler;
    }

    [HttpGet]
    [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> Search([FromQuery] string query)
    {
        var result = await _searchLocationQueryHandler.Handle(new SearchLocationQuery{Query = query});

        return result.GetObjectResult();
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<LocationDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetAll()
    {
        return (await _getAllLocationHandler.Handle(new GetAllLocationQuery())).GetObjectResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(LocationDto), StatusCodes.Status201Created)]
    public async Task<IActionResult> Add([FromBody]LocationDto newLocation)
    {
        var result = await _addLocationCommandHandler.Handle(new AddLocationCommand(newLocation));
        return result.GetObjectResult();
    }
}