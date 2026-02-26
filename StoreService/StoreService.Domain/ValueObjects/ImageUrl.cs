using StoreService.Domain.Shared.Exceptions;

namespace StoreService.Domain.ValueObjects
{
    public record ImageUrl
    {
        public string Value { get; }

        private ImageUrl(string value)
        {
            Value = value;
        }

        public static ImageUrl Create(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new DomainException("Image url cannot be null or empty.");
            }

            return new ImageUrl(value);
        }
    }
}
