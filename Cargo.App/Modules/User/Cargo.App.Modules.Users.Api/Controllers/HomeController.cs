using Microsoft.AspNetCore.Mvc;

namespace Cargo.App.Module.User.Api.Controllers;

[ApiController]
[Route(UsersModule.BasePath)]
public class HomeController : ControllerBase
{
    [HttpGet]
    public OkResult Ping()
    {
        return Ok();
    }
}