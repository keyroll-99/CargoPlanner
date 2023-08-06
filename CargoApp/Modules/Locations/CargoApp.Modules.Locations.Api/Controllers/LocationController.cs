using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route($"{ModuleInstaller.BasePath}/[controller]/[action]")]
public class LocationController : ControllerBase
{
    
    
    [HttpGet("")]
    public IActionResult Search([FromQuery] string query)
    {
        return Ok();
    }
}