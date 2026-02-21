using AuthorizationService.Domain.Interfaces.Services;
using AuthorizationService.Domain.ValueObjects;
using AuthorizationService.Infrastructure.Shared;
using AuthorizationService.Web.Contracts.Authorization;
using AuthorizationService.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace AuthorizationService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(
        IAuthService authService,
        IOptions<JwtOptions> jwtOptions) : ControllerBase
    {
        private readonly IAuthService _authService = authService;
        private readonly JwtOptions _jwtOptions = jwtOptions.Value;

        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            var accessToken = await _authService.LoginAsync(
                EmailAddress.Create(request.EmailAddress), request.Password);

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = false,
                SameSite = SameSiteMode.Strict,
                Expires = DateTimeOffset.UtcNow.AddDays(30)
            };
            HttpContext.Response.Cookies.Append(_jwtOptions.AccessToken.CookieName, accessToken, cookieOptions);
            return NoContent();
        }

        [HttpPost("register")]
        public async Task<IActionResult> RegisterAsync([FromBody] RegisterRequest request)
        {
            await _authService.RegisterAsync(
                EmailAddress.Create(request.EmailAddress), request.Password);
            return NoContent();
        }

        [HttpDelete("logout")]
        [Authorize]
        public IActionResult Logout()
        {
            HttpContext.Response.Cookies.Delete(_jwtOptions.AccessToken.CookieName);
            return NoContent();
        }

        [HttpGet("validate")]
        public IActionResult Validate()
        {
            HttpContext.Response.Headers.Append("X-User-Id", User.GetUserId().ToString());
            return NoContent();
        }
    }
}
