using AuthorizationService.Domain.Shared.Exceptions;

namespace AuthorizationService.Domain.ValueObjects
{
    public record PasswordHash
    {
        public string Hash { get; }

        private PasswordHash(string hash)
        {
            Hash = hash;
        }

        public static PasswordHash Create(string hash)
        {
            if (string.IsNullOrWhiteSpace(hash))
            {
                throw new DomainException("Password hash cannot be null or empty.");
            }

            return new PasswordHash(hash);
        }
    }
}
