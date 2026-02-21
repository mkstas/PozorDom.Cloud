using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AuthorizationService.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(u => u.Id);

            builder.Property(u => u.Id).HasColumnName("id");

            builder.OwnsOne(u => u.EmailAddress, b =>
            {
                b.HasIndex(e => e.Address).IsUnique();
                b.Property(e => e.Address)
                 .IsRequired()
                 .HasMaxLength(EmailAddress.MAX_ADDRESS_LENGTH)
                 .HasColumnName("email_address");
            });

            builder.OwnsOne(u => u.PasswordHash, b =>
            {
                b.Property(p => p.Hash)
                 .IsRequired()
                 .HasColumnName("password_hash");
            });
        }
    }
}
