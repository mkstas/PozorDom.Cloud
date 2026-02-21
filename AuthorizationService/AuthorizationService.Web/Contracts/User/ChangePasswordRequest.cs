using System.ComponentModel.DataAnnotations;

namespace AuthorizationService.Web.Contracts.User
{
    public record ChangePasswordRequest(
        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "Password cannot exceed 32 characters.")]
        string Password,

        [Required(ErrorMessage = "NewPassword is required.")]
        [MinLength(8, ErrorMessage = "New password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "New password cannot exceed 32 characters.")]
        string NewPassword);
}
