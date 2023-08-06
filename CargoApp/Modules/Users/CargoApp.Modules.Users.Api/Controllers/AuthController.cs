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
        if (!result.IsSuccess)
        {
            return result.GetObjectResult();
        }
        
        var userToken = await _refreshTokenService.GenerateTokenAsync(result.SuccessModel!.UserId);
        SetCookie(RefreshTokenCookieName, userToken);

        return result.GetObjectResult();
    }

    [HttpPost]
    [Authorize]
    [ProducesResponseType(typeof(JsonWebToken), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Refresh()
    {
        var cookie = Request.Cookies.TryGetValue(RefreshTokenCookieName, out var refreshToken);
        if (!cookie)
        {
            return BadRequest("Refresh token in cookie not found");
        }

        var result = await _refreshTokenService.RefreshTokenAsync(refreshToken!);
        if (!result.IsSuccess)
        {
            return result.GetObjectResult();
        }
        
        var tokenResult = await _refreshTokenService.GenerateJsonWebTokenAsync(refreshToken!);
        if (tokenResult.IsSuccess)
        {
            SetCookie(RefreshTokenCookieName, result.SuccessModel!);
        }

        return tokenResult.GetObjectResult();

    }

    // Todo: maybe I should create utils for it
    private void SetCookie(string key, string value)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.Now.AddDays(7),
            Secure = true,
            SameSite = SameSiteMode.None
        };
        Response.Cookies.Append(key, value, cookieOptions);
    }
}