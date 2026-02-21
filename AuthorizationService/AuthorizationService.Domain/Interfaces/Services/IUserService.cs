using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.ValueObjects;

namespace AuthorizationService.Domain.Interfaces.Services
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(Guid id);
        Task ChangeEmailAsync(Guid id, EmailAddress email);
        Task ChangePasswordAsync(Guid id, string password, string newPassword);
    }
}
