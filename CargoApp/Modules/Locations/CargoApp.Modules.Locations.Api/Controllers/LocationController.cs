using CargoApp.Modules.Locations.Application.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[action]")]
public class LocationController : ControllerBase
{
    private readonly ISearchLocation _searchLocation;

    public LocationController(ISearchLocation searchLocation)
    {
        _searchLocation = searchLocation;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        return Ok(await _searchLocation.Search(query));
    }
}