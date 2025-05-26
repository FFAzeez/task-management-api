using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeamTaskManagementAPI.Business.Services.Interfaces.Auth;
using TeamTaskManagementAPI.Domain.BindingModels.Requests;
using TeamTaskManagementAPI.Domain.BindingModels.Response;

namespace TeamTaskManagementAPI.Controllers;

[Route("")]
[ApiController]
public class AuthController(IAuthService authService) : BaseController
{

    [HttpPost("api/auth/register")]
    public async Task<ActionResult<BaseResponse>> Register([FromBody] UserRegisterRequest model)
    {
        var user = await authService.RegisterAsync(model);
        return Ok(user);
    }

    [HttpPost("api/auth/login")]
    public async Task<ActionResult<BaseResponse<LoginResponse>>> Login([FromBody] UserLoginRequest model)
    {
        var token = await authService.LoginAsync(model);
        return Ok(token);
    }

    [HttpGet("api/users/me")]
    [Authorize]
    public async Task<ActionResult<BaseResponse<UserResponse>>> GetCurrentUserInfo()
    {
        var userId = Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value).ToString();
        var user = await authService.GetCurrentUserInfoAsync(userId);
        return Ok(user);
    }
}