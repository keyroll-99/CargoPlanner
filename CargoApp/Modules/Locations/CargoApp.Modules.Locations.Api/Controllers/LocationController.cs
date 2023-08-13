using CargoApp.Modules.Locations.Application.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[action]")]
public class LocationController : ControllerBase
{
    private readonly ISearchLocationQuery _searchLocationQuery;

    public LocationController(ISearchLocationQuery searchLocationQuery)
    {
        _searchLocationQuery = searchLocationQuery;
    }

    [HttpGet]
    [Authorize]
    public async Task<IActionResult> Search([FromQuery] string query)
    {
        return Ok(await _searchLocationQuery.Search(query));
    }
}