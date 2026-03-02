using StoreService.Domain.Entities;
using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Interfaces.Services
{
    public interface IDeviceService
    {
        Task CreateDeviceAsync(DeviceTypeId deviceTypeId, Name name, ImageUrl? imageUrl);
        Task<List<Device>> GetDevicesAsync();
        Task<Device?> GetDeviceByIdAsync(DeviceId id);
        Task UpdateDeviceTypeAsync(DeviceId id, DeviceTypeId deviceTypeId);
        Task UpdateDeviceNameAsync(DeviceId id, Name name);
        Task UpdateDeviceImageUrlAsync(DeviceId id, ImageUrl imageUrl);
        Task DeleteDeviceImageUrlAsync(DeviceId id);
    }
}
