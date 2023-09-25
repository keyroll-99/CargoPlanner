using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Auth;
using CargoApp.Core.ShareCore.Enums;
using CargoApp.Modules.Contracts.Users;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Users.Controllers;

[ApiController]
[Authorize]
[Route($"{ModuleInstaller.BasePath}/[controller]")]
public class UserController
{
    private readonly IUser _userService;
    private readonly IContext _context;
    private readonly IPermissionTools _permissionTools;

    public UserController(IUser userService, IContext context, IPermissionTools permissionTools)
    {
        _userService = userService;
        _context = context;
        _permissionTools = permissionTools;
    }

    [HttpGet("Me")]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetLoggedUser()
    {
        var result = await _userService.GetUserByIdAsync(_context.IdentityContext.Id);
        return result.GetObjectResult();
    }

    [HttpPost("[action]")]
    [RequirePermission(PermissionEnum.Workers)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> RemovePermission(UpdatePermissionCommand command)
    {
        var result = await _permissionTools.RemovePermission(command with { AddPermission = false });
        return result.GetObjectResult();
    }

    [HttpPost("[action]")]
    [RequirePermission(PermissionEnum.Workers)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> AddPermission(UpdatePermissionCommand command)
    {
        var result = await _permissionTools.AddPermission(command with { AddPermission = true });
        return result.GetObjectResult();
    }
}