using AuthorizationService.Domain.Entities;
using AuthorizationService.Persistence.Configurations;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Persistence
{
    public class AuthorizationServiceDbContext(
        DbContextOptions<AuthorizationServiceDbContext> options) : DbContext(options)
    {
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new UserConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
