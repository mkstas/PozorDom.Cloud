using AuthorizationService.Domain.Interfaces.Repositories;
using AuthorizationService.Domain.Interfaces.Services;
using AuthorizationService.Domain.ValueObjects;
using AuthorizationService.Infrastructure.Shared;

namespace AuthorizationService.Application.Services
{
    public class AuthService(
        IUserRepository userRepository,
        IJwtProvider jwtProvider) : IAuthService
    {
        private readonly IUserRepository _userRepository = userRepository;
        private readonly IJwtProvider _jwtProvider = jwtProvider;

        public async Task<string> LoginAsync(EmailAddress email, string password)
        {
            var user = await _userRepository.GetUserByEmailAsync(email)
                ?? throw new UnauthorizedAccessException("Incorrect email address or password.");

            if (!PasswordHasher.Verify(password, user.PasswordHash.Hash))
            {
                throw new UnauthorizedAccessException("Incorrect email address or password.");
            }

            return _jwtProvider.GenerateAccessToken(user.Id);
        }

        public async Task RegisterAsync(EmailAddress email, string password)
        {
            var passwordHash = PasswordHasher.Generate(password);
            await _userRepository.CreateUserAsync(email, PasswordHash.Create(passwordHash));
        }
    }
}
