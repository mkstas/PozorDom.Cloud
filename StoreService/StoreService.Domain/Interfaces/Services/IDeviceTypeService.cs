using StoreService.Domain.Entities;
using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Interfaces.Services
{
    public interface IDeviceTypeService
    {
        Task CreateDeviceTypeAsync(Name name);
        Task<List<DeviceType>> GetDeviceTypesAsync();
        Task UpdateDeviceType(DeviceTypeId id, Name name);
    }
}
