using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ScheduleAnywhere.Api.DTOs;
using ScheduleAnywhere.Core.Interfaces;

namespace ScheduleAnywhere.Api.Controllers.Auth;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    /// <summary>Authenticates an employee and returns JWT access and refresh tokens.</summary>
    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await authService.LoginAsync(request.Username, request.Password, ct);
        return Ok(new LoginResponse(result.AccessToken, result.RefreshToken,
            result.ExpiresAt.ToString("O"), result.EmployeeId, result.OrganizationId, result.Roles));
    }

    /// <summary>Exchanges a refresh token for a new access token.</summary>
    [HttpPost("refresh")]
    [AllowAnonymous]
    [ProducesResponseType<LoginResponse>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Refresh([FromBody] RefreshRequest request, CancellationToken ct)
    {
        var result = await authService.RefreshAsync(request.RefreshToken, ct);
        return Ok(new LoginResponse(result.AccessToken, result.RefreshToken,
            result.ExpiresAt.ToString("O"), result.EmployeeId, result.OrganizationId, result.Roles));
    }

    /// <summary>Revokes the current employee's session.</summary>
    [HttpPost("logout")]
    [Authorize]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Logout([FromServices] ICurrentUserService currentUser, CancellationToken ct)
    {
        await authService.LogoutAsync(currentUser.EmployeeId, ct);
        return NoContent();
    }

    /// <summary>Checks whether a username exists in an organization (for the login pre-check).</summary>
    [HttpGet("validate-username")]
    [AllowAnonymous]
    [ProducesResponseType<bool>(StatusCodes.Status200OK)]
    public async Task<IActionResult> ValidateUsername([FromQuery] string username, [FromQuery] int organizationId, CancellationToken ct)
    {
        var exists = await authService.ValidateUsernameAsync(username, organizationId, ct);
        return Ok(exists);
    }
}
