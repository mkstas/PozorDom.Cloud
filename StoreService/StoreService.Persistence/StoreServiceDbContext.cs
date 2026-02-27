using Microsoft.EntityFrameworkCore;
using StoreService.Domain.Entities;
using StoreService.Persistence.Configurations;

namespace StoreService.Persistence
{
    public class StoreServiceDbContext(
        DbContextOptions<StoreServiceDbContext> options) : DbContext(options)
    {
        public DbSet<DeviceType> DeviceTypes { get; set; }
        public DbSet<Device> Devices { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new DeviceTypeConfiguration());
            builder.ApplyConfiguration(new DeviceConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
