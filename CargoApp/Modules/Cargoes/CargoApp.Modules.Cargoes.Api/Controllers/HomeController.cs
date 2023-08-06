using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Cargoes.Api.Controllers;

[Route(ModuleInstaller.BasePath)]
public class HomeController : ControllerBase
{
    [HttpGet]
    public ObjectResult Ping()
    {
        return new OkObjectResult($"Ok from {ModuleInstaller.BasePath}");
    }
}