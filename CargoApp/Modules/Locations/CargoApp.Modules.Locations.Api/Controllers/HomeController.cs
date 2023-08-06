using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Locations.Api.Controllers;

[Route(ModuleInstaller.BasePath)]
public class HomeController : ControllerBase
{
    [HttpGet]
    public static ObjectResult Ping()
    {
        return new OkObjectResult($"Ok from {ModuleInstaller.BasePath}");
    }
}