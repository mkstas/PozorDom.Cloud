using StoreService.Domain.Shared.Exceptions;
using StoreService.Domain.ValueObjects;

namespace StoreService.Domain.Entities
{
    public class Device
    {
        public Guid Id { get; private set; }
        public Guid DeviceTypeId { get; private set; }
        public DeviceType? DeviceType { get; private set; }
        public Name Name { get; private set; }
        public ImageUrl? ImageUrl { get; private set; }

#pragma warning disable CS8618
        private Device() { }
#pragma warning restore CS8618

        private Device(Guid deviceTypeId, Name name, ImageUrl? imageUrl)
        {
            Id = Guid.NewGuid();
            DeviceTypeId = deviceTypeId;
            Name = name;
            ImageUrl = imageUrl;
        }

        public static Device Create(Guid deviceTypeId, Name name, ImageUrl? imageUrl)
        {
            if (deviceTypeId == Guid.Empty)
            {
                throw new DomainException("DeviceTypeId cannot be empty.");
            }

            return new Device(deviceTypeId, name, imageUrl);
        }
    }
}
