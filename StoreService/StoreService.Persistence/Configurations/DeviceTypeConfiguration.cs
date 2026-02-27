using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreService.Domain.Entities;
using StoreService.Domain.ValueObjects;

namespace StoreService.Persistence.Configurations
{
    public class DeviceTypeConfiguration : IEntityTypeConfiguration<DeviceType>
    {
        public void Configure(EntityTypeBuilder<DeviceType> builder)
        {
            builder.ToTable("device_types");
            builder.HasKey(dt => dt.Id);

            builder.Property(dt => dt.Id)
                   .HasColumnName("id");

            builder.OwnsOne(dt => dt.Name, b =>
            {
                b.Property(n => n.Value)
                 .IsRequired()
                 .HasColumnName("name")
                 .HasMaxLength(Name.MAX_NAME_LENGTH);
            });

            builder.HasMany(dt => dt.Devices)
                   .WithOne(d => d.DeviceType)
                   .HasForeignKey(d => d.DeviceTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
