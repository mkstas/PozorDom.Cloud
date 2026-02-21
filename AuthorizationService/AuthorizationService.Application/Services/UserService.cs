using AuthorizationService.Domain.Entities;
using AuthorizationService.Domain.Interfaces.Repositories;
using AuthorizationService.Domain.Interfaces.Services;
using AuthorizationService.Domain.Shared.Exceptions;
using AuthorizationService.Domain.ValueObjects;
using AuthorizationService.Infrastructure.Shared;

namespace AuthorizationService.Application.Services
{
    public class UserService(
        IUserRepository userRepository) : IUserService
    {
        private readonly IUserRepository _userRepository = userRepository;

        public async Task<User> GetUserByIdAsync(Guid id)
        {
            return await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException("User with this id does not exist.");
        }

        public async Task ChangeEmailAsync(Guid id, EmailAddress email)
        {
            await _userRepository.UpdateEmilAsync(id, email);
        }

        public async Task ChangePasswordAsync(Guid id, string password, string newPassword)
        {
            var user = await _userRepository.GetUserByIdAsync(id)
                ?? throw new NotFoundException("User with this id does not exist.");

            if (!PasswordHasher.Verify(password, user.PasswordHash.Hash))
            {
                throw new UnauthorizedAccessException("Password is incorrect.");
            }

            var passwordHash = PasswordHasher.Generate(newPassword);
            await _userRepository.UpdatePasswordAsync(id, PasswordHash.Create(passwordHash));
        }
    }
}
