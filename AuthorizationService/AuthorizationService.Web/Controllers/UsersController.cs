using AuthorizationService.Domain.Interfaces.Services;
using AuthorizationService.Domain.ValueObjects;
using AuthorizationService.Web.Contracts.User;
using AuthorizationService.Web.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AuthorizationService.Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsersController(
        IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet("me")]
        [Authorize]
        public async Task<IActionResult> GetUserMeAsync()
        {
            var user = await _userService.GetUserByIdAsync(User.GetUserId());
            return Ok(new GetUserResponse(user.Id, user.EmailAddress.Address));
        }

        [HttpPatch("me/email")]
        [Authorize]
        public async Task<IActionResult> ChangeEmailAsync([FromBody] ChangeEmailRequest request)
        {
            await _userService.ChangeEmailAsync(
                User.GetUserId(), EmailAddress.Create(request.EmailAddress));
            return NoContent();
        }

        [HttpPatch("me/password")]
        [Authorize]
        public async Task<ActionResult> ChangePasswordAsync([FromBody] ChangePasswordRequest request)
        {
            await _userService.ChangePasswordAsync(
                User.GetUserId(), request.Password, request.NewPassword);
            return NoContent();
        }
    }
}
