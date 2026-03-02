using StoreService.Domain.Entities;
using StoreService.Domain.Interfaces.Repositories;
using StoreService.Domain.Interfaces.Services;
using StoreService.Domain.Shared.Exceptions;
using StoreService.Domain.ValueObjects;

namespace StoreService.Application.Services
{
    public class DeviceTypeService(
        IDeviceTypeRepository deviceTypeRepository) : IDeviceTypeService
    {
        private readonly IDeviceTypeRepository _deviceTypeRepository = deviceTypeRepository;

        public async Task CreateDeviceTypeAsync(Name name)
        {
            await _deviceTypeRepository.CreateDeviceTypeAsync(name);
        }

        public async Task<List<DeviceType>> GetDeviceTypesAsync()
        {
            var deviceTypes = await _deviceTypeRepository.GetDeviceTypesAsync();

            if (deviceTypes.Count == 0)
            {
                throw new NotFoundException("Device types do not exists.");
            }

            return deviceTypes;
        }

        public async Task UpdateDeviceType(DeviceTypeId id, Name name)
        {
            await _deviceTypeRepository.UpdateDeviceType(id, name);
        }
    }
}
