using CargoApp.Modules.Users.Core.DTO;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Users.Controllers;

[ApiController]
[Route($"{ModuleInstaller.BasePath}/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("Me")]
    public Task<UserDto> GetLoggedUser()
    {
        return Task.FromResult(new UserDto(Guid.NewGuid(), "test@test.com", true));
    }
}