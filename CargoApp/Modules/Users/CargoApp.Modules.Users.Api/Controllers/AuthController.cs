using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Context;
using CargoApp.Modules.Contracts.User;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Users.Controllers;

[ApiController]
[Route($"{ModuleInstaller.BasePath}/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
    {
        var result = await _authService.CreateUserAsync(createUserCommand);
        return result.GetObjectResult();
    }

    [HttpPost("[action]")]
    [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn(SingInCommand command)
    {
        return (await _authService.SignInAsync(command)).GetObjectResult();
    }
}