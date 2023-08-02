using CargoApp.Core.Abstraction.Context;
using CargoApp.Modules.Contracts.Users;
using CargoApp.Modules.Contracts.Users.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Users.Controllers;

[ApiController]
[Route($"{ModuleInstaller.BasePath}/[controller]")]
public class UserController
{
    private readonly IUser _userService;
    private readonly IContext _context;

    
    [HttpGet("Me")]
    [Authorize]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoggedUser()
    {
        var result = await _userService.GetUserByIdAsync(_context.IdentityContext.Id);
        return result.GetObjectResult();
    }
}