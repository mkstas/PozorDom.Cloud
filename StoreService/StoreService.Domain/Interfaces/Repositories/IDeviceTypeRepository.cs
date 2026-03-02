using StoreService.Domain.Entities;
using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Interfaces.Repositories
{
    public interface IDeviceTypeRepository
    {
        Task CreateDeviceTypeAsync(Name name);
        Task<List<DeviceType>> GetDeviceTypesAsync();
        Task UpdateDeviceType(DeviceTypeId id, Name name);
    }
}
