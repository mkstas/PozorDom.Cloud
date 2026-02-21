using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.Interfaces.Repositories;
using AuthorizationService.Domain.Shared.Exceptions;
using AuthorizationService.Domain.ValueObjects;
using AuthorizationService.Persistence.Extensions;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationService.Persistence.Repositories
{
    public class UserRepository(AuthorizationServiceDbContext context) : IUserRepository
    {
        private readonly AuthorizationServiceDbContext _context = context;

        public async Task CreateUserAsync(EmailAddress address, PasswordHash hash)
        {
            await _context.AddAsync(User.Create(address, hash));

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation("IX_users_email_address"))
            {
                throw new ConflictException("User with this email address already exists.");
            }
        }

        public async Task<User?> GetUserByIdAsync(Guid id)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User?> GetUserByEmailAsync(EmailAddress address)
        {
            return await _context.Users
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.EmailAddress.Address == address.Address);
        }

        public async Task UpdateEmilAsync(Guid id, EmailAddress address)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("User with this email address does not exist.");

            try
            {
                user.ChangeEmailAddress(address);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex) when (ex.IsUniqueConstraintViolation("IX_users_email_address"))
            {
                throw new ConflictException("User with this email address already exists.");
            }
        }

        public async Task UpdatePasswordAsync(Guid id, PasswordHash hash)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new NotFoundException("User with this email address does not exist.");

            user.ChangePasswordHash(hash);
            await _context.SaveChangesAsync();
        }
    }
}
