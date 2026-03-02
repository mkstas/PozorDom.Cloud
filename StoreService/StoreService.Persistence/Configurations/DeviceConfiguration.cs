using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using StoreService.Domain.Entities;
using StoreService.Domain.ValueObjects;

namespace StoreService.Persistence.Configurations
{
    public class DeviceConfiguration : IEntityTypeConfiguration<Device>
    {
        public void Configure(EntityTypeBuilder<Device> builder)
        {
            builder.ToTable("devices");
            builder.HasKey(d => d.Id);

            builder.Property(d => d.Id)
                   .HasConversion(
                        id => id.Value,
                        value => new DeviceId(value))
                   .HasColumnName("id");

            builder.Property(d => d.DeviceTypeId)
                   .HasConversion(
                        id => id.Value,
                        value => new DeviceTypeId(value))
                   .HasColumnName("device_type_id");

            builder.OwnsOne(d => d.Name, b =>
            {
                b.Property(n => n.Value)
                 .IsRequired()
                 .HasColumnName("name")
                 .HasMaxLength(Name.MAX_LENGTH);
            });

            builder.OwnsOne(d => d.ImageUrl, b =>
            {
                b.Property(i => i.Value)
                 .HasColumnName("image_url");
            });

            builder.HasOne(d => d.DeviceType)
                   .WithMany(dt => dt.Devices)
                   .HasForeignKey(d => d.DeviceTypeId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
