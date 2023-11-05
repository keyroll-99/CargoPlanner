using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Cargoes.Api.Controllers;

[Route(ModuleInstaller.BasePath)]
public class HomeController : ControllerBase
{
    
    /// <summary>
    /// Pings of cargoes method
    /// </summary>
    /// <remarks>test</remarks>
    /// <returns></returns>
    [HttpGet]
    public ObjectResult Ping()
    {
        return new OkObjectResult($"Ok from {ModuleInstaller.BasePath}");
    }
}