using AuthorizationService.Domain.ValueObjects;
using System.Net.Mail;

namespace AuthorizationService.Domain.Entities
{
    public class User
    {
        public Guid Id { get; private set; }
        public EmailAddress EmailAddress { get; private set; }
        public PasswordHash PasswordHash { get; private set; }

#pragma warning disable CS8618
        private User() { }
#pragma warning restore CS8618

        private User(EmailAddress address, PasswordHash passwordHash)
        {
            Id = Guid.NewGuid();
            EmailAddress = address;
            PasswordHash = passwordHash;
        }

        public static User Create(EmailAddress address, PasswordHash hash)
        {
            return new User(address, hash);
        }

        public void ChangeEmailAddress(EmailAddress address)
        {
            EmailAddress = address;
        }

        public void ChangePasswordHash(PasswordHash hash)
        {
            PasswordHash = hash;
        }
    }
}
