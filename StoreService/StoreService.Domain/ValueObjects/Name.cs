using StoreService.Domain.Shared.Exceptions;

namespace StoreService.Domain.ValueObjects
{
    public record Name
    {
        public const int MAX_NAME_LENGTH = 64;

        public string Value { get; }

        private Name(string vlaue)
        {
            Value = vlaue;
        }

        public static Name Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Name cannot be null or empty.");
            }

            if (value.Length > MAX_NAME_LENGTH)
            {
                throw new DomainException($"Name must not exceed {MAX_NAME_LENGTH} characters.");
            }

            return new Name(value);
        }
    }
}
