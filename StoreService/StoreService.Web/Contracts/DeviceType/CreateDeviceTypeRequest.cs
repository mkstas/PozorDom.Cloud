using System.ComponentModel.DataAnnotations;

namespace StoreService.Web.Contracts.DeviceType
{
    public record CreateDeviceTypeRequest(
        [Required]
        [MaxLength(64, ErrorMessage = "Name cannot exceed 64 characters.")]
        string Name);
}
