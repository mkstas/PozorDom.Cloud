using StoreService.Domain.Entities;
using StoreService.Domain.Interfaces.Repositories;
using StoreService.Domain.Interfaces.Services;
using StoreService.Domain.Shared.Exceptions;
using StoreService.Domain.ValueObjects;

namespace StoreService.Application.Services
{
    public class DeviceService(
        IDeviceRepository deviceRepository) : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository = deviceRepository;

        public async Task CreateDeviceAsync(DeviceTypeId deviceTypeId, Name name, ImageUrl? imageUrl)
        {
            await _deviceRepository.CreateDeviceAsync(deviceTypeId, name, imageUrl);
        }

        public Task<Device?> GetDeviceByIdAsync(DeviceId id)
        {
            return _deviceRepository.GetDeviceByIdAsync(id)
                ?? throw new NotFoundException("Device does not exist.");
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            var devices = await _deviceRepository.GetDevicesAsync();

            if (devices.Count == 0)
            {
                throw new NotFoundException("Devices do not exists.");
            }

            return devices;
        }

        public async Task UpdateDeviceImageUrlAsync(DeviceId id, ImageUrl imageUrl)
        {
            await _deviceRepository.UpdateDeviceImageUrlAsync(id, imageUrl);
        }

        public async Task UpdateDeviceNameAsync(DeviceId id, Name name)
        {
            await _deviceRepository.UpdateDeviceNameAsync(id, name);
        }

        public async Task UpdateDeviceTypeAsync(DeviceId id, DeviceTypeId deviceTypeId)
        {
            await _deviceRepository.UpdateDeviceTypeAsync(id, deviceTypeId);
        }

        public async Task DeleteDeviceImageUrlAsync(DeviceId id)
        {
            await _deviceRepository.DeleteDeviceImageUrlAsync(id);
        }
    }
}
