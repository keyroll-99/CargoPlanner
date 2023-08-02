using CargoApp.Core.Abstraction.Auth;
using CargoApp.Core.Abstraction.Context;
using CargoApp.Core.Infrastructure.Response;
using CargoApp.Modules.Contracts.Users.DTO;
using CargoApp.Modules.Users.Core.Commands;
using CargoApp.Modules.Users.Core.Services;
using CargoApp.Modules.Users.Core.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CargoApp.Modules.Users.Controllers;

[ApiController]
[Route($"{ModuleInstaller.BasePath}/[controller]/[action]")]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;
    private readonly IRefreshTokenService _refreshTokenService;
    private const string RefreshTokenCookieName = "refreshToken";

    public AuthController(IAuthService authService, IRefreshTokenService refreshTokenService)
    {
        _authService = authService;
        _refreshTokenService = refreshTokenService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateUser(CreateUserCommand createUserCommand)
    {
        var result = await _authService.CreateUserAsync(createUserCommand);
        return result.GetObjectResult();
    }

    [HttpPost]
    [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> SignIn(SingInCommand command)
    {
        var result = await _authService.SignInAsync(command);
        if (result.IsSuccess)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTimeOffset.Now.AddDays(7),
                Secure = true,
                SameSite = SameSiteMode.None
            };
            var userToken = await _refreshTokenService.GenerateTokenAsync(result.SuccessModel!.UserId);
            Response.Cookies.Append(RefreshTokenCookieName, userToken, cookieOptions);
        }

        return result.GetObjectResult();
    }
}