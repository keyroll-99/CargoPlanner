using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Users.Controllers;

[ApiController]
[Route($"{ModuleInstaller.BasePath}/[controller]/[action]")]
public class PasswordRecoveryController : ControllerBase
{
    private readonly IPasswordRecoveryService _passwordRecoveryService;

    public PasswordRecoveryController(IPasswordRecoveryService passwordRecoveryService)
    {
        _passwordRecoveryService = passwordRecoveryService;
    }


    [HttpPost("")]
    public async Task<IActionResult> InitPasswordRecovery(InitPasswordRecoveryCommand command)
    {
        var result = await _passwordRecoveryService.InitPasswordRecovery(command);
        return result.GetObjectResult();
    }

    [HttpGet("")]
    public async Task<IActionResult> IsRecoveryKeyValid([FromQuery] string recoveryKey)
    {
        return (await _passwordRecoveryService.IsRecoveryKeyValid(recoveryKey)).GetObjectResult();
    }

    [HttpPatch("{recoveryHash:guid}")]
    public async Task<IActionResult> ChangePassword(Guid recoveryHash, [FromBody] ChangePasswordCommand command)
    {
       return (await _passwordRecoveryService.ChangePassword(recoveryHash, command)).GetObjectResult();
    }
}