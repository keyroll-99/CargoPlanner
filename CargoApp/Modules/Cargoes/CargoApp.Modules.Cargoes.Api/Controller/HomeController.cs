using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Cargoes.Api.Controller;

[Route(ModuleInstaller.BasePath)]
public class HomeController : ControllerBase
{
    public ActionResult<string> Ping()
    {
        return new OkObjectResult($"Ok from {ModuleInstaller.BasePath}");
    }
}