using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.DTO;
using CargoApp.Modules.Users.Core.Services;
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

    [HttpGet("Me")]
    public Task<UserDto> GetLoggedUser()
    {
        return Task.FromResult(new UserDto(Guid.NewGuid(), "test@test.com", true));
    }

    [HttpPost("Create")]
    public async Task<UserDto> CreateUser(CreateUserCommand createUserCommand)
    {
        return await _authService.CreateUserAsync(createUserCommand);
    }
}