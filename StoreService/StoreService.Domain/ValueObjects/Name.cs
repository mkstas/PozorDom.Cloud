using StoreService.Domain.Shared.Exceptions;

namespace StoreService.Domain.ValueObjects
{
    public record Name
    {
        public const int MAX_LENGTH = 64;

        public string Value { get; }

        private Name(string value) => Value = value;

        public static Name Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Name cannot be null or empty.");
            }

            if (value.Length > MAX_LENGTH)
            {
                throw new DomainException($"Name must not exceed {MAX_LENGTH} characters.");
            }

            return new Name(value);
        }
    }
}
