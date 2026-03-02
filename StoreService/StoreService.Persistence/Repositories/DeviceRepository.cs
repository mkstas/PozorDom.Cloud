using Microsoft.EntityFrameworkCore;
using StoreService.Domain.Entities;
using StoreService.Domain.Interfaces.Repositories;
using StoreService.Domain.Shared.Exceptions;
using StoreService.Domain.ValueObjects;

namespace StoreService.Persistence.Repositories
{
    public class DeviceRepository(
        StoreServiceDbContext context) : IDeviceRepository
    {
        private readonly StoreServiceDbContext _context = context;

        public async Task CreateDeviceAsync(DeviceTypeId deviceTypeId, Name name, ImageUrl? imageUrl)
        {
            await _context.AddAsync(Device.Create(deviceTypeId, name, imageUrl));
            await _context.SaveChangesAsync();
        }

        public async Task<List<Device>> GetDevicesAsync()
        {
            return await _context.Devices
                .AsNoTracking()
                .ToListAsync();
        }


        public async Task<Device?> GetDeviceByIdAsync(DeviceId id)
        {
            return await _context.Devices
                .AsNoTracking()
                .FirstOrDefaultAsync(d => d.Id == id);
        }

        public async Task UpdateDeviceTypeAsync(DeviceId id, DeviceTypeId deviceTypeId)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new NotFoundException("Device does not exist.");

            var deviceType = await _context.DeviceTypes.FirstOrDefaultAsync(u => u.Id == deviceTypeId)
                ?? throw new NotFoundException("Device type does not exist.");

            device.ChangeDeviceType(deviceTypeId);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceNameAsync(DeviceId id, Name name)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new NotFoundException("Device does not exist.");

            device.ChangeName(name);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateDeviceImageUrlAsync(DeviceId id, ImageUrl imageUrl)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new NotFoundException("Device does not exist.");

            device.ChangeImageUrl(imageUrl);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDeviceImageUrlAsync(DeviceId id)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.Id == id)
                ?? throw new NotFoundException("Device does not exist.");

            device.DeleteImageUrl();
            await _context.SaveChangesAsync();
        }
    }
}
