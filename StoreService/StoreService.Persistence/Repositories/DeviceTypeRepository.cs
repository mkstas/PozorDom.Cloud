using Microsoft.EntityFrameworkCore;
using StoreService.Domain.Entities;
using StoreService.Domain.Interfaces.Repositories;
using StoreService.Domain.Shared.Exceptions;
using StoreService.Domain.ValueObjects;

namespace StoreService.Persistence.Repositories
{
    public class DeviceTypeRepository(
        StoreServiceDbContext context) : IDeviceTypeRepository
    {
        private readonly StoreServiceDbContext _context = context;

        public async Task CreateDeviceTypeAsync(Name name)
        {
            await _context.AddAsync(DeviceType.Create(name));
            await _context.SaveChangesAsync();
        }

        public async Task<List<DeviceType>> GetDeviceTypesAsync()
        {
            return await _context.DeviceTypes
                .AsNoTracking()
                .ToListAsync();
        }

        public async Task UpdateDeviceType(DeviceTypeId id, Name name)
        {
            var deviceType = await _context.DeviceTypes.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("Device type does not exist.");

            deviceType.ChangeName(name);
            await _context.SaveChangesAsync();
        }
    }
}
