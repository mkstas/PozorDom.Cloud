using AuthorizationService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationService.Web.Contracts.Authorization
{
    public record RegisterRequest(
        [Required(ErrorMessage = "EmailAddress is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_ADDRESS_LENGTH,
            ErrorMessage = "EmailAddress cannot exceed 254 characters.")]
        string EmailAddress,

        [Required(ErrorMessage = "Password is required.")]
        [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
        [MaxLength(32, ErrorMessage = "Password cannot exceed 32 characters.")]
        string Password);
}
