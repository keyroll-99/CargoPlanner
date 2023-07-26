using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Context;
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
    private readonly IContext _context;

    public AuthController(IAuthService authService, IContext context)
    {
        _authService = authService;
        _context = context;
    }

    [HttpGet("Me")]
    [Authorize]
    public Task<OkObjectResult> GetLoggedUser()
    {
        
        return Task.FromResult(new OkObjectResult(_context.IdentityContext));
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