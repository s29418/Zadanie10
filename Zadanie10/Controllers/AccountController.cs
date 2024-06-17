using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Zadanie10.DTO_s;
using Zadanie10.Services;

namespace Zadanie10.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AccountController : ControllerBase
{
    private readonly IAccountService _accountService;

    public AccountController(IAccountService accountService)
    {
        _accountService = accountService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterRequest model)
    {
        var result = await _accountService.RegisterUserAsync(model);
        if (!result)
        {
            return BadRequest("Registration failed.");
        }
        return Ok("Registration successful.");
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest loginRequest)
    {
        var result = await _accountService.AuthenticateUserAsync(loginRequest);
        if (!result.Success)
        {
            return Unauthorized("Invalid login attempt.");
        }
        return Ok(new
        {
            accessToken = result.AccessToken,
            refreshToken = result.RefreshToken
        });
    }

    [Authorize(AuthenticationSchemes = "IgnoreTokenExpirationScheme")]
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh(RefreshTokenRequest refreshTokenRequest)
    {
        var result = await _accountService.RefreshTokenAsync(refreshTokenRequest);
        if (!result.Success)
        {
            return Unauthorized("Invalid or expired refresh token.");
        }
        return Ok(new
        {
            accessToken = result.AccessToken,
            refreshToken = result.RefreshToken
        });
    }
}