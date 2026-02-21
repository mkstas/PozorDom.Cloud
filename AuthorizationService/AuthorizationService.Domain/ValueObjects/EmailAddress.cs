using AuthorizationService.Domain.Shared.Exceptions;
using System.Net.Mail;

namespace AuthorizationService.Domain.ValueObjects
{
    public record EmailAddress
    {
        public const int MAX_ADDRESS_LENGTH = 254;

        public string Address { get; }

        private EmailAddress(string address)
        {
            Address = address;
        }

        public static EmailAddress Create(string address)
        {
            if (address.Any(char.IsWhiteSpace))
            {
                throw new DomainException("Email address must not contain whitespace characters.");
            }

            if (address.Length > MAX_ADDRESS_LENGTH)
            {
                throw new DomainException($"Email address must not exceed {MAX_ADDRESS_LENGTH} characters.");
            }

            try
            {
                _ = new MailAddress(address);
                return new EmailAddress(address);
            }
            catch (FormatException)
            {
                throw new DomainException("Invalid email address format.");
            }
        }
    }
}
