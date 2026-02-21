using AuthorizationService.Domain.ValueObjects;

namespace AuthorizationService.Domain.Interfaces.Services
{
    public interface IAuthService
    {
        Task RegisterAsync(EmailAddress email, string password);
        Task<string> LoginAsync(EmailAddress email, string password);
    }
}
