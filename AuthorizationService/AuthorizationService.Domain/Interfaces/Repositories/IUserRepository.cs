using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.ValueObjects;

namespace AuthorizationService.Domain.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task CreateUserAsync(EmailAddress address, PasswordHash hash);
        Task<User?> GetUserByIdAsync(Guid id);
        Task<User?> GetUserByEmailAsync(EmailAddress address);
        Task UpdateEmilAsync(Guid id, EmailAddress address);
        Task UpdatePasswordAsync(Guid id, PasswordHash hash);
    }
}
