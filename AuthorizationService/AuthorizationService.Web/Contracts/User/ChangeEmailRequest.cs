using AuthorizationService.Domain.ValueObjects;
using System.ComponentModel.DataAnnotations;

namespace AuthorizationService.Web.Contracts.User
{
    public record ChangeEmailRequest(
        [Required(ErrorMessage = "EmailAddress is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [MaxLength(
            EmailAddress.MAX_ADDRESS_LENGTH,
            ErrorMessage = "EmailAddress cannot exceed 254 characters.")]
        string EmailAddress);
}
